using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookList.Models;
using System.Web.Http;

namespace BookList.Interface
{
    interface IBook
    {
        /// <summary>
        /// 获取所有书籍
        /// </summary>
        /// <returns></returns>
        IEnumerable<Book> getBooks();

        /// <summary>
        /// 根据书籍id获取书本信息
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Book getBookWithId(int bookId);

        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="book"></param>
        void addBook(Book book);

        /// <summary>
        /// 根据书本id删除书籍
        /// </summary>
        /// <param name="bookId"></param>
        void deleteBookWithBookId(int bookId);

        /// <summary>
        /// 更新书籍
        /// </summary>
        /// <param name="book"></param>
        void updateBook(Book book);
    }
}
