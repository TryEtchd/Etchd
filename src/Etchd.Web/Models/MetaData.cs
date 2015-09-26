using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etchd.Web.Models
{
    public class MetaData
    {
        public DateTime PublishDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Author Author { get; set; } = new Author();

        public virtual ICollection<string> Tags { get; set; } = new List<string>();

        public string MetaDataTitle { get; set; }
    }
}