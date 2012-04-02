using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GPXtoCOTs_Converter
{
    public partial class Form1 : Form
    {
        public string line = null;
        public string new_string = null;
        public string project_name = null;
        private string file;
        private int current_line_number = 0;
        public string latitude;
        public string longitude;
        public string time;
        public string stale_time;
        public int how_long_for_stale = 40;
        public bool first_line = true;

            public Form1()
            {
                InitializeComponent();
            }

            public void Open_File_Click(object sender, EventArgs e)
            {
                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    file = openFileDialog1.FileName;
                }

            }

            private void parseToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (file != null)
                {
                    try
                    {
                        write_event();

                        using (StreamReader read_line = new StreamReader(file))
                        {
                            while ((line = read_line.ReadLine()) != null)
                            {
                                parse_txt();
                                current_line_number++;
                            }
                        }
                    }
                    catch (IOException)
                    {
                    }

                    write_to_text_box(); // outputs new_string to text box
                }
            }

        public void write_to_text_box()
        {
            text_box.Text = new_string;
        }

        public void save_parsed_file()
        {
            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter write_stream = new StreamWriter(fs);
            write_stream.Write(new_string);
            write_stream.Flush();
            fs.Close();       
        }
        
        private void left_justify()
        {
            while(line.StartsWith("\t") == true)
            {
                line = line.Replace("\t", "");
            }
        }

        public void write_event()
        {
            if (first_line == false)
                new_string = new_string + "\r\n";

            new_string = new_string + "<?xml version='1.0' standalone='yes'?>\r\n<event version=\"2.0\" uid=\"****\" how=\"m-r\" time=\"" + time + "\" stale=\"" + stale_time + "\" type=\"a-f-G\" start=\"" + time + "\">\r\n<detail></detail>\r\n<point hae=\"0\" lat=\"" + latitude + "\" lon=\"" + longitude + "\" ce=\"25\" le=\"10\" />\r\n</event>";
            first_line = false;
        }

        //**** Main logic of how to parse the text ****
        public void parse_txt()
        {
            left_justify();

            if (line.Length == 0) // are we processing a line that is blank?
                ;
            else if (line.StartsWith("<?xml") == true)
                ; // do nothing
            else if (line.StartsWith("<gpx") == true)
                ; // do nothing
            else if (line.StartsWith("<trk>") == true)
                ; // do nothing
            else if (line.StartsWith("<name>") == true)
                ; // do nothing
            else if (line.StartsWith("<desc>") == true)
                ; // do nothing
            else if (line.StartsWith("<trkseg>") == true)
                ; // do nothing
            else if (line.StartsWith("<trkpt") == true)
                get_lat_long();
            else if (line.StartsWith("<ele>") == true)
                ; // do nothing, we dont' want elevation for this project
            else if (line.StartsWith("<time>") == true)
                get_time();
            else if (line.StartsWith("</trkpt>") == true)
                write_event();
            else if (line.StartsWith("</trkseg>") == true)
                ; // do nothing
            else if (line.StartsWith("</trk>") == true)
                ; // do nothing
            else if (line.StartsWith("</gpx>") == true)
                ; // do nothing
            else
                MessageBox.Show("unknown line " + line.ToString() + "(" + current_line_number + ")");

        }

        private void get_lat_long()
        {
            int wheres_first_quote = line.IndexOf('"');
            int wheres_sec_quote = line.IndexOf(('"'), (wheres_first_quote + 1));
            int wheres_third_quote = line.IndexOf(('"'), wheres_sec_quote + 1);
            int wheres_fourth_quote = line.IndexOf(('"'), wheres_third_quote + 1);

            latitude = line.Substring(wheres_first_quote + 1, (wheres_sec_quote - 1) - wheres_first_quote);
            longitude = line.Substring(wheres_third_quote + 1, (wheres_fourth_quote - 1) - wheres_third_quote);
        }

        private void get_time()
        {
            int wheres_first_great_bracket = line.IndexOf('>');
            int wheres_sec_less_bracket = line.IndexOf('<', wheres_first_great_bracket);
            time = line.Substring(wheres_first_great_bracket + 1, (wheres_sec_less_bracket-wheres_first_great_bracket)-1);

            set_stale_time();
        }

        private void set_stale_time()
        {
            string min = (time.Substring(time.IndexOf(':')+1, 2));
            int new_time = Convert.ToInt32(min) + how_long_for_stale;
            min = new_time.ToString();
            stale_time = time.Remove(time.IndexOf(':')+1, 2);
            stale_time = stale_time.Insert(stale_time.IndexOf(':')+1, min);
        }

        //// change < & > to xml escape sequence
        //private string replace_xml_symbols(string _txt)
        //{
        //    if (_txt.IndexOf('<') > 0)
        //    {
        //        while (_txt.IndexOf('<') > 0)
        //        {
        //            _txt = _txt.Insert(_txt.IndexOf('<'), "&lt;");
        //            _txt = _txt.Remove(_txt.IndexOf('<'), 1);
        //        }
        //    }

        //    if (_txt.IndexOf('>') > 0)
        //    {
        //        while (_txt.IndexOf('>') > 0)
        //        {
        //            _txt = _txt.Insert(_txt.IndexOf('>'), "&gt;");
        //            _txt = _txt.Remove(_txt.IndexOf('>'), 1);
        //        }
        //    }

        //    return _txt;
        //}

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Save_File_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = project_name;
            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {

                file = saveFileDialog1.FileName;
                save_parsed_file();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_box_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

