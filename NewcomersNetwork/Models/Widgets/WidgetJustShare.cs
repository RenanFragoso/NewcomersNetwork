using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models.Widgets
{
    public class WidgetJustShare : NNAPIModel
    {
        public List<JustShareCategory> Categories;
        
        public WidgetJustShare()
        {
        }
    }

    public class JustShareCategory
    {
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public List<JustShareClassifiedWidget> Classifieds;

        public JustShareCategory()
        {
        }

        public JustShareCategory(string categoryId, string categoryName)
        {
            this.CategoryName = categoryName;
            this.CategoryId = categoryId;
            this.Classifieds = new List<JustShareClassifiedWidget>();
        }

        public void AddWidget(JustShareClassifiedWidget widget)
        {
            this.Classifieds.Add(widget);
        }

    }

    public class JustShareClassifiedWidget
    {
        private string _Id;
        
        public string Id {  get { return _Id; }
                            set { _Id = value; }
        }
        public string Title { get; set; } = "";
        public string Location { get; set; } = "";

        public string Link {
            get { return "/justshare/" + Convert.ToBase64String(Encoding.Default.GetBytes(_Id)); }
        }

        public string Image {
            get { return ImageControlStatic.RetrieveImage("classifieds", _Id); }
        }
        
        public JustShareClassifiedWidget()
        {
        }

        public JustShareClassifiedWidget(string id, string title)
        {
            this.Id = id;
            this.Title = title;
            //this.Location = location;
        }

    }

}