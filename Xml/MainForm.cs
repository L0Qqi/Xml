using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Drawing.Imaging;

namespace Xml
{
    public partial class MainForm : Form
    {
        public string file;
        public XmlDocument xDoc = new XmlDocument();
        List<MusObject> List = new List<MusObject>();
        public int num;
        public MainForm()
        {
            xDoc.Load(@"./../../musObjects.xml");
            
            InitializeComponent();
            GetXMLfile();
            CreateColumns();
            LoadToDG(List);
            dataGridView1.Hide();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("Type", "Тип");
            dataGridView1.Columns.Add("Name", "Название");
            dataGridView1.Columns.Add("Author", "Автор");
            dataGridView1.Columns.Add("Duration", "Время производства");
            dataGridView1.Columns.Add("PublishDate", "Дата создания");
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            
        }

        
        private void GetXMLfile()
        {
            XmlElement root = xDoc.DocumentElement;
            ParseMat(root);
            
        }
        private void ParseMat(XmlElement root)
        {
            foreach (XmlElement node in root)
            {
                if (node.Name == "musobject")
                {
                    MusObject j = new MusObject();
                    int cnt = 0;
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        cnt++;
                        switch (child.Name)
                        {
                            case "type":
                                j.type = $"{child.InnerText}";
                                break;
                            case "author":
                                j.name = $"{(child.FirstChild.InnerText)}";
                                j.author = $"{(child.LastChild.InnerText)}";
                                break;
                            case "duration":
                                j.duration = child.InnerText;
                                break;
                            case "publishedDate":
                                j.publishDate = child.InnerText;
                                break;
                            default:
                                break;
                        }
                        if (cnt == 5)
                        {
                            List.Add(j);
                            cnt = 0;
                            j = new MusObject();
                        }
                    }
                }
            }
        }
        
        

        private void LoadToDG(List<MusObject> list)
        {
            foreach (MusObject musObject in list)
            {
                dataGridView1.Rows.Add(musObject.type, musObject.name, musObject.author, musObject.duration);
            }
        }
       
    }
}
