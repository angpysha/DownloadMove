using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoverLib
{
    public class MoverLib
    {
        public delegate void TestingEventHandler(string mes);
        public event TestingEventHandler TestEvent;
        private FileSystemWatcher fw = new FileSystemWatcher();
        SettingsLib.Settings.Setts setts;
        public MoverLib()
        {
            SettingsLib.Settings.category categ = new SettingsLib.Settings.category();

            setts = categ.deserialize();
            /*var configfilewatcher = new FileSystemWatcher(SettingsLib.Constants.savefile);
            configfilewatcher.Changed += delegate
            {
                Stop();
                setts = categ.deserialize();
                Init(setts);
                Start();
            };*/
            Init(setts);
           /* fw = new FileSystemWatcher(setts.inputfolder);
            fw.Changed += new FileSystemEventHandler(f_chan);
            fw.Created += new FileSystemEventHandler(f_cre);*/
        }
        private void Init(SettingsLib.Settings.Setts setts)
        {
            fw.Path = setts.inputfolder;
            fw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
            fw.Filter = "*.*";
            fw.Changed += new FileSystemEventHandler(f_chan);
            //fw.EnableRaisingEvents = true;
           // fw.Created += new FileSystemEventHandler(f_cre);
        }
        private void f_chan(object sender, FileSystemEventArgs e)
        {
            Thread t = new Thread(async () => await MoveFile(e.FullPath,setts));
            t.Start();
           // MessageBox.Show(e.Name.EndsWith);

        }

        async Task MoveFile(string file,SettingsLib.Settings.Setts setts)
        {
            List<SettingsLib.Settings.Sets> ls = setts.sets;
            var s = file.Split('.');
            string d = "." + s.Last();
           if (ls.Any(ss=>ss.extentions.Split(',').ToList().Contains(d)))
            {
                if (!isFileLocked(file))
                {
                    //MessageBox.Show("fds");
                    string outp = (ls.Where(ss => ss.extentions.Split(',').ToList().Contains(d)).First()).path;
                    string filename = file.Split('\\').Last();
                    if (!Directory.Exists(outp))
                        Directory.CreateDirectory(outp);
                    //MessageBox.Show(filename);
                    try
                    {
                        File.Move(file, Path.Combine(outp, filename));
                        if (TestEvent !=null)
                        {
                            TestEvent(String.Format("{0} -> {1}", outp, filename));
                        }
                    } catch (Exception ex)
                    {

                    }
                  // MessageBox.Show(outp);
                }
            }
        }
        private void f_cre(object sender, FileSystemEventArgs e)
        {
            TestEvent(e.Name);
        }

        public void Start()
        {
            fw.EnableRaisingEvents = true;
        }
        public bool isFileLocked(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
            }

            catch (Exception exp)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }
        public bool geteventstate()
        {
            return fw.EnableRaisingEvents;
        }
        public void Stop()
        {
            fw.EnableRaisingEvents = false;
        }
    }
}
