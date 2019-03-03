using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp9
{
    public partial class Form2 : Form
    {
        List<song> fav;                                                                            //A list of song type variables (From song.cs class), fav.
        public Form2(List<song> top10)                                                             //Form2 initialize along with a list of song type variables (From song.cs class), top10.                          
        {
            InitializeComponent();                                                                 //Initialize components. 
            fav = new List<song>();                                                                //fav, is a list of song type variables (From song.cs class).                     
            fav = top10;                                                                           //Lists of song type variables (From song.cs class), fav equals to top10 initially.
        }

        private void Form2_Load(object sender, EventArgs e)                                        //Form2_Load void.     
        {
            try                                                                                    //A try method.
            {
                if (fav.Count() <= 10)                                                             //If fav.Count is less than/equals to 10, then: 
                {
                    for (int i = 0; i < fav.Count(); i++)                                          //A for loop statement. 
                    {
                        StreamWriter sw = new StreamWriter("tmp.txt", true);                       //A StreamWriter variable, sw for tmp.txt file. 
                        {
                            sw.Write("SONG : " + fav[i].Song + "  ARTIST : " + fav[i].Artist +     //StreamWriter variable, sw writes to tmp.txt file,  
                                     "  ALBUM : " + fav[i].Album + "  YEAR : " + fav[i].Year +     //song's name, song's artist, song's publish year, 
                                     "  GENDER : " + fav[i].Gender + "  CLICKS : " + fav[i].Fame   //song's gender category and song's clicks (plays),
                                     + Environment.NewLine);                                       //which all are provided from song.cs class.
                        }
                        sw.Close();                                                                //StreamWriter variable, sw closes.          
                    }
                }
                else                                                                               //Else (If fav.Count is over 10) then:                      
                {
                    for (int i = 0; i < 10; i++)                                                   //A for loop statement (with 10 loops).                          
                    {
                        StreamWriter sw = new StreamWriter("tmp.txt", true);                       //A StreamWriter variable, sw for tmp.txt file.                                      
                        {
                            sw.Write("SONG : " + fav[i].Song + "  ARTIST : " + fav[i].Artist +     //StreamWriter variable, sw writes to tmp.txt file,                           
                                     "  ALBUM : " + fav[i].Album + "  YEAR : " + fav[i].Year +     //song's name, song's artist, song's publish year,                                  
                                     "  GENDER : " + fav[i].Gender + "  CLICKS : " + fav[i].Fame   //song's gender category and song's clicks (plays),                                     
                                     + Environment.NewLine);                                       //which all are provided from song.cs class.            
                        }
                        sw.Close();                                                                //StreamWriter variable, sw closes.     
                    }
                }
                richTextBox1.LoadFile("tmp.txt", RichTextBoxStreamType.PlainText);                 //richTextBox1 loads tmp.txt file.                     
                File.Delete("tmp.txt");                                                            //Deletes tmp.txt file.         
            }
            catch                                                                                  //Exception.                                                 
            {
                MessageBox.Show("THERE ARE NO SONGS");                                             //Exception message appears.                                 
                this.Close();                                                                      //Closes the Application (Form2).                     
            }
        }
    }
}
