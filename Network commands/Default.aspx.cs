using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Network_commands
{
    public partial class Default : System.Web.UI.Page
    {
        static List<string> fileContents = new List<string>();
        static List<string> jpgFileNames = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fileContents = Directory.GetFiles(@"C:\Users\muhan\source\repos\Network commands\Network commands\Data", "*.txt")
                                   .Select(File.ReadAllText)
                                   .ToList(); List<int> fileNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                jpgFileNames = Directory.GetFiles(@"C:\Users\muhan\source\repos\Network commands\Network commands\Data", "*.jpg")
                                      .Select(Path.GetFileName)
                                      .ToList();
                var rgx = new Regex("[^a-zA-Z]");
                while (fileNumbers.Count > 0)
                {
                    foreach (var fileText in fileContents)
                    {

                        var fileNumberString = fileText.Substring(0, 2).Contains(".") ? fileText.Substring(0, 1) : fileText.Substring(0, 2);
                        var fileNumber = int.Parse(fileNumberString);
                        var controlId = fileText.Substring(0, 3);
                        var fileNumberIndex = fileNumbers.IndexOf(fileNumber);
                        if (fileNumberIndex != 0 || FindControl(controlId) != null) continue;
                        var temp = fileText.Split(new[] { "-" }, StringSplitOptions.None);
                        var t = temp[1].Split(new[] { "Example: " }, StringSplitOptions.None);
                        var title = rgx.Replace(temp[0], "");
                        var img = jpgFileNames[fileContents.IndexOf(fileText)];
                        var image = new Image();
                        var h1 = new HtmlGenericControl("h1")
                        {
                            InnerText = Char.ToUpper(title[0]) + title.Substring(1, title.Length - 1)
                        };
                        image.ImageUrl = $"~/Data/{img}";
                        div1.Controls.Add(h1);
                        div1.Controls.Add(image);
                        div1.Controls.Add(new HtmlGenericControl("br"));
                        div1.Controls.Add(new Label { Text = temp[1] + "\n", ID = controlId });
                        div1.Controls.Add(new HtmlGenericControl("br"));
                        div1.Controls.Add(new Label { Text = "Example: " + t[0] + "\n", ID = controlId + "d" });
                        div1.Controls.Add(new HtmlGenericControl("br"));
                        div1.Controls.Add(new HtmlGenericControl("br"));
                        fileNumbers.RemoveAt(fileNumberIndex);
                    }
                }
            }
        }
    }
}