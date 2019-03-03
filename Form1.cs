using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();                                               //Random type variable, rnd.
        song music;                                                              //Song type variable music.                                
        List<song> playlist = new List<song>();                                  //List of song (From song.cs class) type variables, playlist.                            
        List<song> top10 = new List<song>();                                     //List of song (From song.cs class) type variables, top10.                                                  
        public Form1()
        {
            InitializeComponent();                                               //Initialize components.         
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Supported Audio | *.mp3; *.wma | MP3s | *.mp3 | WMAs | *.wma";
            //Open only the supported audio files.
            string textLine;                                                     //String type variable, textLine.                           
            try
            {
                StreamReader sr = new StreamReader("playlist.txt");              //StreamReader type variable, sr for playlist.txt file.
                {
                    while ((textLine = sr.ReadLine()) != null)                   //A while loop, while textline variable is not null.       
                    {
                       string[] songInformation = textLine.Split(',');           //An array of string type variables, songInformation, which gets the textLine
                                                                                 //content splitted by ','.                       
                       music = new song()                                        //Initialize music, a song type variable.                       
                            .addSong(songInformation[0])                         //addSong gets songInformation's first string.                           
                            .addArtist(songInformation[1])                       //addArtist gets songInformation's second string.                             
                            .addAlbum(songInformation[2])                        //addAlbum gets songInformation's third string.                             
                            .addYear(songInformation[3])                         //addYear gets songInformation's forth string.                                         
                            .addGender(songInformation[4])                       //addGender gets songInformation's fifth string.                                         
                            .addSource(songInformation[5])                       //addSource gets songInformation's sixth string.                                             
                            .addFame(Convert.ToInt32(songInformation[6]));       //addFame gets a 32-bit integer after corventing songInformation's seventh string.                                            
                        playlist.Add(music);                                     //List of song type variables, playlist adds song type variable music.           
                        
                        ListViewItem item = new ListViewItem();                  //A ListViewItem variable, item.                                   
                        item.Text = music.Song;                                  //ListViewItem variable, item gets music.Song content.                       
                        item.SubItems.Add(music.Artist);                         //ListViewItem variable, item gets music.Artist content.                                                   
                        item.SubItems.Add(music.Album);                          //ListViewItem variable, item gets music.Album content.                                   
                        item.SubItems.Add(music.Year);                           //ListViewItem variable, item gets music.Year content.                                           
                        item.SubItems.Add(music.Gender);                         //ListViewItem variable, item gets music.Gender content.                                                       
                        item.SubItems.Add(music.Source);                         //ListViewItem variable, item gets music.Soutce content.                                       
                        item.SubItems.Add(music.Fame.ToString());                //ListViewItem variable, item gets, string-converted music.Fame content.                                                       
                        listView1.Items.Add(item);                               //ListViewItem variable, item added to listView1.                                               
                    }
                }
                sr.Close();                                                      //StreamReader type variable, sr closes.                       
            }
            catch (FileNotFoundException fnf)                                    //Exception.                       
            {
                Console.WriteLine("File does not exist " + fnf);                 //Exception's message appears.                                                   
            }   
           
        }
        private void button2_Click(object sender, EventArgs e)                   //Search void (button2_Click).
        {

            foreach (ListViewItem lvi in listView1.Items)                        //Foreach ListViewItem variable, lvi in listView1.Items.
            {
                if (lvi.SubItems[0].Text == textBox6.Text)                       //If lvi.SubItems[0] text equals to search box (textBox6.Text), then:                           
                {
                    axWindowsMediaPlayer1.URL = lvi.SubItems[5].Text;            //WindowsMediaPlayer1 gets the url of the song.                          
                    axWindowsMediaPlayer1.Ctlcontrols.play();                    //WindowsMediaPlayer1 plays the song.                                       

                    MessageBox.Show("SONG : " + lvi.SubItems[0].Text   + Environment.NewLine
                                  + "ARTIST : " + lvi.SubItems[1].Text + Environment.NewLine 
                                  + "ALBUM : " + lvi.SubItems[2].Text  + Environment.NewLine 
                                  + "YEAR : " + lvi.SubItems[3].Text   + Environment.NewLine 
                                  + "GENDER : " + lvi.SubItems[4].Text + Environment.NewLine 
                                  + "SOURCE : " + lvi.SubItems[5].Text);         //A MessageBox appears, showing song;s informations.  
                    
                    lvi.Selected = true;                                         //ListViewItem variable, lvi is selected.                 
                    lvi.Focused = true;                                          //ListViewItem variable, lvi is focused.                         
                }
            }  
        }
        private void button5_Click(object sender, EventArgs e)                   //Edit void (button5_Click).           
        {
            try                                                                  //Try method.
            {
                if (textBox1.Text == "")                                         //If textBox1.Text is empty, then:                             
                {
                    textBox1.BackColor = System.Drawing.Color.Red;               //The textBox1's back color changes to red.                                   
                    textBox1.ForeColor = System.Drawing.Color.White;             //The textBox1's font color changes to white. 

                    MessageBox.Show("ADD SONG NAME", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Error MessageBox appears.
                    {
                        textBox1.BackColor = System.Drawing.Color.White;         //After the MessageBox disappears the textBox1's back color changes to white.                         
                        textBox1.ForeColor = System.Drawing.Color.Black;         //After the MessageBox disappears the textBox1's font color changes to black.                                    
                    }
                }

                else                                                             //Else if textBox1.Text is not empty, then:                    
                {
                    playlist[listView1.SelectedItems[0].Index].Song = textBox1.Text;                     //playlist.Song of listView1 selected item equals to textBox1's text.                          
                    playlist[listView1.SelectedItems[0].Index].Artist = textBox2.Text;                   //playlist.Artist of listView1 selected item equals to textBox2's text.                     
                    playlist[listView1.SelectedItems[0].Index].Album = textBox3.Text;                    //playlist.Album of listView1 selected item equals to textBox3's text.                                     
                    playlist[listView1.SelectedItems[0].Index].Year = textBox4.Text;                     //playlist.Year of listView1 selected item equals to textBox4's text.                                 
                    playlist[listView1.SelectedItems[0].Index].Gender = textBox5.Text;                   //playlist.Gender of listView1 selected item equals to textBox5's text.                                     
                    playlist[listView1.SelectedItems[0].Index].Source = openFileDialog1.FileName;        //playlist.Source of listView1 selected item equals to openFileDialog1's filename.    

                    listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;                         //First SubItem's text from the selected Items of listView1 equals to textBox1's text.                           
                    listView1.SelectedItems[0].SubItems[1].Text = textBox2.Text;                         //Second SubItem's text from the selected Items of listView1 equals to textBox2's text.                            
                    listView1.SelectedItems[0].SubItems[2].Text = textBox3.Text;                         //Third SubItem's text from the selected Items of listView1 equals to textBox3's text.                                
                    listView1.SelectedItems[0].SubItems[3].Text = textBox4.Text;                         //Forth SubItem's text from the selected Items of listView1 equals to textBox4's text.                                        
                    listView1.SelectedItems[0].SubItems[4].Text = textBox5.Text;                         //Fifth SubItem's text from the selected Items of listView1 equals to textBox5's text.                    
                    listView1.SelectedItems[0].SubItems[5].Text = openFileDialog1.FileName;              //Sixth SubItem's text from the selected Items of listView1 equals to openFileDialog1's FileName.    

                    File.WriteAllText("playlist.txt", String.Empty);                                     //Writes to playlist.txt file.

                    using (StreamWriter sw = new StreamWriter("playlist.txt", true))                     //Using a StreamWriter variable, sw for playlist.txt file.                        
                    {                                                       
                        foreach (song p in playlist)                                                     //A foreach statement.                 
                        {
                            sw.WriteLine(p.Song + "," + p.Artist + ","                           //StreamWriter variable, sw writes to playlist.txt file,                                                   
                                        + p.Album + "," + p.Year + ","                           //song's name, song's artist, song's album, song's publish year,                                            
                                        + p.Gender + "," + p.Source + ","                        //song's gender category, song's source and song's fame (plays),                                                          
                                        + p.Fame);                                                   //which all are provided from song.cs class.       
                        }   
                    }
                    MessageBox.Show("SONG EDITED SUCCESSFULLY");                 //A MessageBox appears (Success message).                 
                }
            }
            catch                                                                //Exception.                          
            {
                MessageBox.Show("THERE ARE NO SONGS");                           //A MessageBox appears (Exception message).                                                       
                textBox1.Clear();                                                //textBox1 clears its content.     
            }   
        }
        private void button1_Click_1(object sender, EventArgs e)                 //Add void (button1_Click).                                           
        {
            if (textBox1.Text == "")                                             //If textBox1.Text is empty, then:                                                     
            {
                textBox1.BackColor = System.Drawing.Color.Red;                   //The textBox1's back color changes to red.              
                textBox1.ForeColor = System.Drawing.Color.White;                 //The textBox1's font color changes to white. 

                MessageBox.Show("ADD SONG NAME", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Error MessageBox appears.
                {
                    textBox1.BackColor = System.Drawing.Color.White;             //After the MessageBox disappears the textBox1's back color changes to white.   
                    textBox1.ForeColor = System.Drawing.Color.Black;             //After the MessageBox disappears the textBox1's font color changes to black.   
                }
            }

            else if (openFileDialog1.FileName == "")                             //Else if openFileDialog1 filename is empty, then:
            {
                MessageBox.Show("ADD SONG FILE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Error MessageBox appears.
            }

            else                                                                 //Else:    
            {
                music = new song()                                               //music is a new song (From song.cs class) instance.                       
               .addSong(textBox1.Text)                                           //music.addSong gets textBox1's text.                               
               .addArtist(textBox2.Text)                                         //music.addArtist gets textBox2's text.                                        
               .addAlbum(textBox3.Text)                                          //music.addAlbum gets textBox3's text.                                            
               .addYear(textBox4.Text)                                           //music.addYear gets textBox4's text.                    
               .addGender(textBox5.Text)                                         //music.addGender gets textBox5's text.                                
               .addSource(openFileDialog1.FileName)                              //music.addSource gets source file's name.                                    
               .addFame(1);                                                      //music.addFame adds 1.                            

                ListViewItem item = new ListViewItem(music.Song);                //ListViewItem variable, item is a ListViewItem variable, which contains music.Song.                        
                item.SubItems.Add(music.Artist);                                 //ListViewItem variable, item gets music.Artist content.                                                   
                item.SubItems.Add(music.Album);                                  //ListViewItem variable, item gets music.Album content.                                   
                item.SubItems.Add(music.Year);                                   //ListViewItem variable, item gets music.Year content.                                           
                item.SubItems.Add(music.Gender);                                 //ListViewItem variable, item gets music.Gender content.                                                       
                item.SubItems.Add(music.Source);                                 //ListViewItem variable, item gets music.Soutce content.                                       
                item.SubItems.Add(music.Fame.ToString());                        //ListViewItem variable, item gets, string-converted music.Fame content.                                                       
                listView1.Items.Add(item);                                       //ListViewItem variable, item added to listView1.
                playlist.Add(music);                                             //List of song variables (from song.cs class), playlist adds music.        

                textBox1.Clear();                                                //Clears textBox1.       
                textBox2.Clear();                                                //Clears textBox2.                                  
                textBox3.Clear();                                                //Clears textBox3.                                          
                textBox4.Clear();                                                //Clears textBox4.                              
                textBox5.Clear();                                                //Clears textBox5.                                      
                openFileDialog1.FileName = "";                                   //Clears openFileDialog1's filename (the deleted file).                                                         

                StreamWriter sw = new StreamWriter("playlist.txt", true);        //A StreamWriter variable, sw for playlist.txt file.             
                {
                    sw.WriteLine(music.Song + "," + music.Artist + ","           //StreamWriter variable, sw writes to playlist.txt file,                                                   
                                + music.Album + "," + music.Year + ","           //song's name, song's artist, song's album, song's publish year,                                            
                                + music.Gender + "," + music.Source + ","        //song's gender category, song's source and song's fame (plays),                                                          
                                + music.Fame);                                   //which all are provided from song.cs class.                            
                }
                sw.Close();                                                      //StreamWriter variable, sw closes.                                  
                MessageBox.Show("SONG ADDED SUCCESSFULLY");                      //A MessageBox appears (Success message).                              
            }

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)                 //listview1 selected song void (listView1_SelectedIndexChanged).
        {
            try                                                                                 //A try method.                
            {
                textBox1.Text = playlist[listView1.SelectedItems[0].Index].Song;                //textBox1's text gets playlist.Song of listView1 selected item.            
                textBox2.Text = playlist[listView1.SelectedItems[0].Index].Artist;              //textBox2's text gets playlist.Artist of listView1 selected item.                                      
                textBox3.Text = playlist[listView1.SelectedItems[0].Index].Album;               //textBox3's text gets playlist.Album of listView1 selected item.                                      
                textBox4.Text = playlist[listView1.SelectedItems[0].Index].Year;                //textBox4's text gets playlist.Year of listView1 selected item.                                          
                textBox5.Text = playlist[listView1.SelectedItems[0].Index].Gender;              //textBox5's text gets playlist.Gender of listView1 selected item.                                                      
                openFileDialog1.FileName = playlist[listView1.SelectedItems[0].Index].Source;   //openFileDialog1's FileName is provided by playlist.Source of listView1 selected item.                                          
            }
            catch                                                                               //Exception.                        
            {
                
            }
        }
        private void button4_Click(object sender, EventArgs e)                   //Delete void (button4_Click).
        {
            textBox1.Clear();                                                    //Clears textBox1.       
            textBox2.Clear();                                                    //Clears textBox2.                                  
            textBox3.Clear();                                                    //Clears textBox3.                                          
            textBox4.Clear();                                                    //Clears textBox4.                              
            textBox5.Clear();                                                    //Clears textBox5.                                      
            openFileDialog1.FileName = "";                                       //Clears openFileDialog1's filename (the deleted file).                                   

            try                                                                  //A try method.               
            {
                listView1.SelectedItems[0].Remove();                             //Removes first selected item of listView.                           
                playlist.Clear();                                                //Clears playlist.                                   
                string textLine;                                                 //A string variable, textLine.     
                
                File.WriteAllText("playlist.txt", String.Empty);                 //Writes to playlist.txt file.                                                            
                StreamWriter sw = new StreamWriter("playlist.txt", true);        //A StreamWriter variable, sw for playlist.txt file.                                      

                foreach (ListViewItem lvi in listView1.Items)                    //A foreach statement.                                                       
                {
                    sw.WriteLine(lvi.SubItems[0].Text + ","                      //StreamWriter variable, sw writes to playlist.txt file: the first ListView SubItem,                                        
                               + lvi.SubItems[1].Text + ","                      //the second ListView SubItem,                                                          
                               + lvi.SubItems[2].Text + ","                      //the third ListView SubItem,                                                      
                               + lvi.SubItems[3].Text + ","                      //the forth ListView SubItem,                                                                          
                               + lvi.SubItems[4].Text + ","                      //the fifth ListView SubItem,                                                                      
                               + lvi.SubItems[5].Text + ","                      //the sixth ListView SubItem,                                                          
                               + lvi.SubItems[6].Text);                          //and the seventh ListView SubItem,                                          
                }

                sw.Close();                                                      //StreamWriter variable, sw closes.      
                MessageBox.Show("SONG DELETED SUCCESSFULLY");                    //A MessageBox appears (Delete's success message).   

                StreamReader sr = new StreamReader("playlist.txt");              //A StreamReader variable, sr read playlist.txt file.             
                {
                    while ((textLine = sr.ReadLine()) != null)                   //A while statement.                                                       
                    {
                        string[] songInformation = textLine.Split(',');          //An array of string type variables, songInformation splitted by ",".                   
                        
                    music = new song()                                           //music is a new song (From song.cs class) instance.                                      
                         .addSong(songInformation[0])                            //music.addSong gets songInformation's first element.                                    
                         .addArtist(songInformation[1])                          //music.addArtist gets songInformation's second element.                                                       
                         .addAlbum(songInformation[2])                           //music.addAlbum gets songInformation's third element.                                        
                         .addYear(songInformation[3])                            //music.addYear gets songInformation's forth element.                                           
                         .addGender(songInformation[4])                          //music.addGender gets songInformation's fifth element.                                                          
                         .addSource(songInformation[5])                          //music.addSource gets songInformation's sixth element.                                    
                         .addFame(Convert.ToInt32(songInformation[6]));          //music.addFame gets int converted songInformation's seventh element.                         
                    playlist.Add(music);                                         //List of song variables (from song.cs class), playlist adds music.                                          
                    }
                }
                sr.Close();                                                      //StreamReader variable, sr closes.                                               
            }
            catch                                                                //Exception.       
            {
                MessageBox.Show("NO SONG SELECTED");                             //Exception's message appears.                           
            }
        }

        string oldText = string.Empty;                                           //A string variable oldText, initialize as an empty string.        
        
        private void textBox4_TextChanged(object sender, EventArgs e)            //Song's publish year textBox void (textBox4_TextChanged).   
        {
            if (textBox4.Text.All(chr => char.IsNumber(chr)))                    //textBox4's text (Song's publish year) contains only number type characters.
            {
                oldText = textBox4.Text;                                         //String variable, oldText gets textBox4's text.
                textBox4.Text = oldText;                                         //textBox4's text equals to string variable oldText.                                                
                textBox4.BackColor = System.Drawing.Color.White;                 //textBox4's backcolor changes to white.                                                           
                textBox4.ForeColor = System.Drawing.Color.Black;                 //textBox4's forecolor changes to black.                                            
            }
            else                                                                 //Else:                       
            {
                textBox4.Text = oldText;                                         //textBox4's text equals to string variable oldText.                                            
                textBox4.BackColor = System.Drawing.Color.Red;                   //textBox4's backcolor changes to red.                                                                            
                textBox4.ForeColor = System.Drawing.Color.White;                 //textBox4's forecolor changes to white.                                                        

                MessageBox.Show("USE ONLY NUMBER IN YEARS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Error message appears.
                {
                    textBox4.BackColor = System.Drawing.Color.White;             //textBox4's backcolor changes to white.                                    
                    textBox4.ForeColor = System.Drawing.Color.Black;             //textBox4's forecolor changes to black.                                                
                }
                textBox4.SelectionStart = textBox4.Text.Length;                  //textBox4's selection.                                                   
            }
        }
        private void button3_Click(object sender, EventArgs e)                   //Add song button void (button3_Click).                                           
        {
            openFileDialog1.ShowDialog();                                        //Opens FileDialog in order to select a song.                                              
        }
        private void button6_Click(object sender, EventArgs e)                   //Shuffle button void (button6_Click).                                                               
        {
            int shuffle = rnd.Next(0, listView1.Items.Count);                    //An int type variable, shuffle selects randomly an integer between 0 to listView1's max integer, in order to be contained every listView1 item.                                                               
            axWindowsMediaPlayer1.URL = listView1.Items[shuffle].SubItems[5].Text;//WindowsMediaPlayer1 gets the randomly selected URL.                                         
        }
        private void listView1_DoubleClick(object sender, EventArgs e)           //ListView1 Double Click void.                                          
        {
            string textLine;                                                     //A string variable, textLine.                                                              

            axWindowsMediaPlayer1.URL = playlist[listView1.SelectedItems[0].Index].Source.ToString();                             //WindowsMediaPlayer1 gets the url of the song.        
            axWindowsMediaPlayer1.Ctlcontrols.play();                                                                             //WindowsMediaPlayer1 plays the song.      
            
            playlist[listView1.SelectedItems[0].Index].Song = textBox1.Text;                                                      //playlist.Song of listView1 selected item equals to textBox1's text.                          
            playlist[listView1.SelectedItems[0].Index].Artist = textBox2.Text;                                                    //playlist.Artist of listView1 selected item equals to textBox2's text.                     
            playlist[listView1.SelectedItems[0].Index].Album = textBox3.Text;                                                     //playlist.Album of listView1 selected item equals to textBox3's text.                                     
            playlist[listView1.SelectedItems[0].Index].Year = textBox4.Text;                                                      //playlist.Year of listView1 selected item equals to textBox4's text.                                 
            playlist[listView1.SelectedItems[0].Index].Gender = textBox5.Text;                                                    //playlist.Gender of listView1 selected item equals to textBox5's text.                                     
            playlist[listView1.SelectedItems[0].Index].Source = openFileDialog1.FileName;                                         //playlist.Source of listView1 selected item equals to textBox6's text.                                                     
            playlist[listView1.SelectedItems[0].Index].Fame = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text)+1;     //playlist.Fame is provided from seventh SubItems of listView1 selected item increased by one.

            listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;                                                          //First SubItem's text from the selected Items of listView1 equals to textBox1's text.                           
            listView1.SelectedItems[0].SubItems[1].Text = textBox2.Text;                                                          //Second SubItem's text from the selected Items of listView1 equals to textBox2's text.                            
            listView1.SelectedItems[0].SubItems[2].Text = textBox3.Text;                                                          //Third SubItem's text from the selected Items of listView1 equals to textBox3's text.                                
            listView1.SelectedItems[0].SubItems[3].Text = textBox4.Text;                                                          //Forth SubItem's text from the selected Items of listView1 equals to textBox4's text.                                        
            listView1.SelectedItems[0].SubItems[4].Text = textBox5.Text;                                                          //Fifth SubItem's text from the selected Items of listView1 equals to textBox5's text.                    
            listView1.SelectedItems[0].SubItems[5].Text = openFileDialog1.FileName;                                               //Sixth SubItem's text from the selected Items of listView1 equals to openFileDialog1's FileName.                                        
            listView1.SelectedItems[0].SubItems[6].Text = playlist[listView1.SelectedItems[0].Index].Fame.ToString();             //Seventh SubItem's text from the selected Items of listView1 is provided from string converted playlist.Fame of first selected Item of listView1.                            

            File.WriteAllText("playlist.txt", String.Empty);                     //Writes to playlist.txt file.                                                                               

            using (StreamWriter sw = new StreamWriter("playlist.txt", true))     //Using a StreamWriter variable, sw for playlist.txt file.                                                              
            {
                foreach (song p in playlist)                                     //A foreach statement.                                                               
                {
                    sw.WriteLine(p.Song + "," + p.Artist + ","                   //StreamWriter variable, sw writes for every,  
                               + p.Album + "," + p.Year + ","                    //p in playlist Song, Artist, Album, Year,                            
                               + p.Gender + "," + p.Source + "," + p.Fame);      //Gender, Source and Fame of the song.   
                }
            }

            try                                                                  //A try method.   
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)             //A for loop statement.                           
                {
                        listView1.Items[i].Remove();                             //Removes listView1's selected item.                       
                        playlist.Clear();                                        //Clears playlist.                       
                }
            }
            catch                                                                //Exception.   
            {

            }

            StreamReader sr = new StreamReader("playlist.txt");                  //A StreamReader variable, sr read playlist.txt file.                                      
            {   
                while ((textLine = sr.ReadLine()) != null)                       //A while statement.                                                       
                {
                    string[] songInformation = textLine.Split(',');              //An array of string type variables, songInformation splitted by ",".   
                    
                    music = new song()                                           //music is a new song (From song.cs class) instance.                                      
                         .addSong(songInformation[0])                            //music.addSong gets songInformation's first element.                                    
                         .addArtist(songInformation[1])                          //music.addArtist gets songInformation's second element.                                                       
                         .addAlbum(songInformation[2])                           //music.addAlbum gets songInformation's third element.                                        
                         .addYear(songInformation[3])                            //music.addYear gets songInformation's forth element.                                           
                         .addGender(songInformation[4])                          //music.addGender gets songInformation's fifth element.                                                          
                         .addSource(songInformation[5])                          //music.addSource gets songInformation's sixth element.                                    
                         .addFame(Convert.ToInt32(songInformation[6]));          //music.addFame gets int converted songInformation's seventh element.                         
                    playlist.Add(music);                                         //List of song variables (from song.cs class), playlist adds music.                                  

                    ListViewItem item = new ListViewItem();                      //A ListViewItem variable, item.                                  
                    item.Text = music.Song;                                      //ListViewItem variable, item gets music.Song content.                                                  
                    item.SubItems.Add(music.Artist);                             //ListViewItem variable, item gets music.Artist content.                                               
                    item.SubItems.Add(music.Album);                              //ListViewItem variable, item gets music.Album content.                                                    
                    item.SubItems.Add(music.Year);                               //ListViewItem variable, item gets music.Year content.                                          
                    item.SubItems.Add(music.Gender);                             //ListViewItem variable, item gets music.Gender content.                                                        
                    item.SubItems.Add(music.Source);                             //ListViewItem variable, item gets music.Soutce content.                                                               
                    item.SubItems.Add(music.Fame.ToString());                    //ListViewItem variable, item gets, string-converted music.Fame content.                                              
                    listView1.Items.Add(item);                                   //ListViewItem variable, item added to listView1.                                     
                }
            }
            sr.Close();                                                          //StreamReader variable, sr closes.                           
        }
        private void button8_Click(object sender, EventArgs e)                   //Top10 button void (button8_Click).                   
        {
            string textLine;                                                     //A string variable, textLine.           
            StreamReader sr = new StreamReader("playlist.txt");                  //A StreamReader variable, sr read playlist.txt file.                                      
            {
                top10 = new List<song>();                                        //A list of song variables (from song.cs class), top10.                               
                while ((textLine = sr.ReadLine()) != null)                       //A while statement.                                              
                {
                        string[] songInformation = textLine.Split(',');          //An array of string type variables, songInformation splitted by ",".                                         
                        music = new song()                                       //music is a new song (From song.cs class) instance.                                                  
                             .addSong(songInformation[0])                        //music.addSong gets songInformation's first element.                                      
                             .addArtist(songInformation[1])                      //music.addArtist gets songInformation's second element.                                     
                             .addAlbum(songInformation[2])                       //music.addAlbum gets songInformation's third element.                                               
                             .addYear(songInformation[3])                        //music.addYear gets songInformation's forth element.                                    
                             .addGender(songInformation[4])                      //music.addGender gets songInformation's fifth element.                                              
                             .addSource(songInformation[5])                      //music.addSource gets songInformation's sixth element.                                                 
                             .addFame(Convert.ToInt32(songInformation[6]));      //music.addFame gets int converted songInformation's seventh element.                     
                        top10.Add(music);                                        //List of song variables (from song.cs class), top10 adds music.                                             
                }
            }
            top10 = top10.OrderByDescending(x => x.Fame).ToList();               //List of song variables (from song.cs class), top10 is sorted.                                                  
            sr.Close();                                                          //StreamReader variable, sr closes.     

            Form2 fav = new Form2(top10);                                        //Creates a new instance of the Form2 class, favs and includes to it list of song type variables, top10.                                                                            
            fav.ShowDialog();                                                    //Opens fav (Form2).
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)     //Exit menu button void.
        {
            Application.Exit();                                                  //Exits from the Application.
        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)    //About menu button void.
        {
            MessageBox.Show("This game was made by Panagiotis Apostolopoulos, Dimitris Matsanganis and Pavlos Roumeliotis.");
            //A Messagebox pops up with editors's name.
        }

        private void tOP10ToolStripMenuItem_Click(object sender, EventArgs e)    //Top10 menu button void. 
        {
            this.button8.PerformClick();                                         //At every click calls Top10 button void (button8_Click).
        }

        private void aDDToolStripMenuItem_Click(object sender, EventArgs e)     //Add menu button void.
        {
            this.button1.PerformClick();                                        //At every click calls Add button void (button1_Click).
        }

        private void eDITToolStripMenuItem_Click(object sender, EventArgs e)    //Edit menu button void.
        {
            this.button5.PerformClick();                                        //At every click calls Edit button void (button5_Click).
        }

        private void rEMOVEToolStripMenuItem_Click(object sender, EventArgs e)  //Remove menu button void.
        {
            this.button4.PerformClick();                                        //At every click calls Remove button void (button4_Click).
        }
    }
}                 