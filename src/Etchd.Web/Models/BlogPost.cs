using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Etchd.Web.Models
{
    public class BlogPost : IComparer<BlogPost>, IComparable<BlogPost>
    {
        public BlogPost(MetaData data)
        {
            Author = data.Author;
            PublishDate = data.PublishDate;
            UpdatedDate = data.UpdatedDate;
            Tags = data.Tags;
        }

        [Key]
        public int id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Author Author { get; set; } = new Author();

        public DateTime PublishDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<string> Tags { get; set; } = new List<string>();

        public string url { get; set; }

        private string _metaDataTitle;

        public string MetaDataTitle
        {
            get
            {
                return string.IsNullOrWhiteSpace(_metaDataTitle) ? Title : _metaDataTitle;
            }

            set
            {
                _metaDataTitle = value;
            }
        }

        private string _metaDataBody;

        public string MetaDataBody
        {
            get
            {
                return string.IsNullOrWhiteSpace(_metaDataBody) ? Title : _metaDataBody;
            }

            set
            {
                _metaDataBody = value;
            }
        }

        public int Compare(BlogPost x, BlogPost y)
        {
            return x.PublishDate.CompareTo(y.PublishDate);
        }

        public int CompareTo(BlogPost other)
        {
            return PublishDate.CompareTo(other.PublishDate);
        }

        public string HtmlContent()
        {
            return CommonMark.CommonMarkConverter.Convert(Content);
        }

        #region FutureFeatures

        public bool isStatic { get; set; }
        public bool isFeatured { get; set; }

        public string PostImage { get; set; }

        #endregion FutureFeatures
    }
}