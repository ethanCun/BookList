using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookList.Interface;
using BookList.Models;

namespace BookList.Controllers
{
    public class BooksController : ApiController
    {
        //书架管理器
        public static BookManager manager = new BookManager();

        /// <summary>
        /// 获取所有书籍
        /// </summary>
        /// 
        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return manager.getBooks();
        }

        /// <summary>
        /// 根据书籍id获取书本信息
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public Book PostBookWithId(int bookId)
        {
            return manager.getBookWithId(bookId);
        }

        /// <summary>
        /// 根据bookId删除书籍信息
        /// </summary>
        /// <param name="bookId"></param>
        /// 
        public void RemoveBookWithId(int bookId)
        {
            manager.deleteBookWithBookId(bookId);
        }

        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="book"></param>
        [HttpPost]
        public void AddBook(Book book)
        {
            manager.addBook(book);
        }

        /// <summary>
        /// 更新书本信息
        /// </summary>
        /// <param name="book"></param>
        public void UpdateBook(Book book)
        {
            manager.updateBook(book);
        }
    }
}
