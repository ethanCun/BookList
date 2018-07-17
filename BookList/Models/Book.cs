using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookList.Models
{
    public class Book
    {
        /// <summary>
        /// 书籍id
        /// </summary>
        public int bookId { get; set; }

        /// <summary>
        /// 书籍名称
        /// </summary>
        public string bookName { get; set; }

        /// <summary>
        /// 书籍价格
        /// </summary>
        public string bookPrice { get; set; }

        /// <summary>
        /// 书籍作者
        /// </summary>
        public string bookAuthor { get; set; }
    }
}