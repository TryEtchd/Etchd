using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etchd.Web.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public sealed class Todo : Attribute
    {
        public string GithubIssue { get; set; }
        public FutureMilestone Milestone { get; set; }

        public Todo(FutureMilestone milestone, string githubIssue = "")
        {
            Milestone = milestone;
            GithubIssue = githubIssue;
        }
    }

    public enum FutureMilestone
    {
        Themeing,
        Admin
    }
}