using Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuizDubena.Model
{
    public class CategoryAPIModel : APIModel
    {
        public Category Result { get; set; }

        public CategoryAPIModel()
        {
        }

        public CategoryAPIModel(Category category)
        {
            Result = category;
        }
    }

    public class CategoryListModel : APIModel
    {
        public List<Category> Result
        { get; set; }
    }
}