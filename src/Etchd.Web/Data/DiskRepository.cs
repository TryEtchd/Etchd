using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Etchd.Framework.Extensions;
using Etchd.Web.Models;

namespace Etchd.Data
{
    public class DiskRepository
    {
        private string PathToPosts { get; set; }

        public DiskRepository(string pathToPosts)
        {
            if(!Directory.Exists(pathToPosts))
            {
                Directory.CreateDirectory(pathToPosts);
            }
            PathToPosts = pathToPosts;
        }

        public async Task<IList<BlogPost>> GetPosts()
        {
            await FixFileSystem();
            return new List<BlogPost>();
        }

        /// <summary>
        /// Make sure MD files have json files and vise-versa
        /// </summary>
        private async Task FixFileSystem()
        {
            var mdFiles = Directory.GetFiles(PathToPosts, "*.md", SearchOption.AllDirectories);
            var jsonFiles = Directory.GetFiles(PathToPosts, "*.md", SearchOption.AllDirectories);
            var intersection = mdFiles.Select(a => a.Split('.')[0]).Intersect(jsonFiles.Select(a => a.Split('.')[0]));
            foreach(var intersect in intersection)
            {
                mdFiles.Where(a => !a.StartsWith(intersect, StringComparison.Ordinal)).ForEach(a => File.Create(Path.Combine(PathToPosts, a)));
                foreach(var file in jsonFiles.Where(a => !a.StartsWith(intersect, StringComparison.Ordinal)))
                {
                    using(var writer = File.CreateText(Path.Combine(PathToPosts, file)))
                    {
                        await writer.WriteLineAsync("{");
                        await writer.WriteLineAsync("}");
                        await writer.FlushAsync();
                    }
                }
            }
        }
    }
}