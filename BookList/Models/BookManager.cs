using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookList.Interface;
using System.Data.SqlClient;
using System.Data;
using BookList.Models;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net.Http;

namespace BookList.Models
{
    public class BookManager : IBook
    {
        /// <summary>
        /// 装所有书籍的list
        /// </summary>
        public List<Book> books = new List<Book>();

        /// <summary>
        /// 连接到sqlserver
        /// </summary>
        public void connectDb()
        {
            string connectionStr = "Server=127.0.0.1;user=sa;pwd=sa;database=books";

            SqlConnection sqlCon = new SqlConnection(connectionStr);

            try
            {
                sqlCon.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from mybook", sqlCon);

                DataSet ds = new DataSet();

                sqlDataAdapter.Fill(ds, "mybook");

                this.printDB(ds);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message.ToString());
            }
            finally
            {
                //sqlCon.Close();
            }
        }

        /// <summary>
        /// 打印数据库中的数据
        /// </summary>
        /// <param name="ds"></param>
        public void printDB(DataSet ds)
        {
            //清空
            this.books.Clear();

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Book book = new Book();
                    book.bookId = (int)dr[0];
                    book.bookName = dr[1].ToString();
                    book.bookPrice = dr[2].ToString();
                    book.bookAuthor = dr[3].ToString();

                    this.books.Add(book);
                }
            }
        }


        /// <summary>
        /// 根据id删除书籍
        /// </summary>
        /// <param name="bookId">书籍id</param>
        [HttpPost]
        public void deleteBookWithBookId(int bookId)
        {
            Book bk = new Book();
            foreach(Book book in this.books)
            {
                if(book.bookId == bookId)
                {
                    bk = book;
                }
            }

            this.books.Remove(bk);

            //同步删除数据库中的数据
            string con = "Server=127.0.0.1; user=sa;pwd=sa;database=books";

            SqlConnection sqlcon = new SqlConnection(con);

            sqlcon.Open();

            string sql = "delete from mybook where id = " + bookId;

            SqlCommand sqlCommand = new SqlCommand(sql, sqlcon);

            sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取所有的书籍
        /// </summary>
        /// <returns>所有书籍</returns>
        public IEnumerable<Book> getBooks()
        {
            //连接数据库
            this.connectDb();

            return this.books;
        }

        /// <summary>
        /// 根据id查找书籍
        /// </summary>
        /// <param name="bookId">书籍id</param>
        /// <returns>查找结果</returns>
        public Book getBookWithId(int bookId)
        {
            Book bk;
            foreach(Book book in this.books)
            {
                if(book.bookId == bookId)
                {
                    bk = book;

                    return bk;
                }
            }

            return null;
        }


        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="book">书籍信息</param>
        public void addBook([FromBody]Book book)
        {
            //if(book == null)
            //{
            //    return Ok("添加失败");
            //}

            this.books.Add(book);

            //同步增加数据库中的数据
            string con = "Server=127.0.0.1; user=sa;pwd=sa;database=books";

            SqlConnection sqlcon = new SqlConnection(con);

            sqlcon.Open();

            //注意字符串拼接
            string sql = "insert into mybook (name, price, author) values(" + "'" + book.bookName + "'" + "," + "'" + book.bookPrice + "'" + ","
            + "'" + book.bookAuthor +"'" + ")";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlcon);

            sqlCommand.ExecuteNonQuery();

            //return Ok("添加成功");
        }

        /// <summary>
        /// 更新书籍的信息
        /// </summary>
        /// <param name="book">书籍信息</param>
        public void updateBook([FromBody]Book book)
        {
            //if(book == null)
            //{
            //    return Ok("更新失败");
            //}

            //复制出一个数组 用于操作
            List<Book> newBooks = new List<Book>(this.books.ToArray());
            foreach (Book bk in newBooks)
            {
                if (bk.bookId == book.bookId)
                {
                    this.books.Remove(bk);
                    this.books.Add(book);
                }
            }

            newBooks = null;

            string conStr = "Server=127.0.0.1;user=sa;pwd=sa;database=books";

            SqlConnection sqlcon = new SqlConnection(conStr);

            sqlcon.Open();

            string sql = "update mybook set name = " + "'" + book.bookName + "'" + ", price=" + "'" + book.bookPrice + "'" + "," + "author=" + "'" + book.bookAuthor + "'" + " where id=" + "'" + book.bookId + "'";

            SqlCommand sqlCommand = new SqlCommand(sql, sqlcon);

            sqlCommand.ExecuteNonQuery();

            //return Ok("更新成功");
        }
    }
}