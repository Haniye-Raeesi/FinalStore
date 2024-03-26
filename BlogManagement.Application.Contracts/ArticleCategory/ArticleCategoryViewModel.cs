using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
   public class ArticleCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string CreationDate { get; set; }
        public long ArticleCounts { get; set; }
    }
}
