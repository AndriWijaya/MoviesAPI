using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using TestAPI.Model;
using TestAPI.Helper;
using Dapper;
using static Dapper.SqlMapper;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private const string _connectionString = "server=127.0.0.1;uid=root;pwd=;database=tesapi;";
        private AllMessage _msg;

        public MoviesController() {
            _msg = new AllMessage();
        }

        // GET api/movies
        [HttpGet]
        public ActionResult<List<Film>> Get()
        {
            List<Film> ret = new List<Film>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"select * from film";
                    ret = conn.Query<Film>(sql).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            return ret;
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public ActionResult<Film> Get(int id)
        {
            Film ret = new Film();
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"select * from film where id = @id";
                    ret = conn.QueryFirstOrDefault<Film>(sql, new { id = id });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            return ret;
        }

        // POST api/movies
        [HttpPost]
        public MessageInfo Post([FromBody] Film value)
        {
            MessageInfo messageInfo = new MessageInfo();

            if (string.IsNullOrEmpty(value.Title))
            {
                messageInfo.Message.Add("Title tidak boleh kosong");
            } else if (value.Title.Length > 255)
            {
                messageInfo.Message.Add("Maksimal panjang Title adalah 255 karakter");
            }
            if (value.Description.Length > 1000)
            {
                messageInfo.Message.Add("Maksimal panjang Description adalah 1000 karakter");
            }
            if (value.Image.Length > 255)
            {
                messageInfo.Message.Add("Maksimal panjang Image adalah 255 karakter");
            }

            if (!messageInfo.Message.Any())
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        string sql = @"INSERT INTO film(title, description, rating, image, created_at, updated_at) 
                                    VALUES (@title, @description, @rating, @image, @created_at, @updated_at)";
                        conn.Execute(sql, value);

                        messageInfo.Status = true;
                        messageInfo.Message.Add(_msg.SUCCESS_INSERT);
                    }
                    catch (Exception ex)
                    {
                        messageInfo.Status = false;
                        messageInfo.Message.Add(_msg.FAIL_INSERT);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return messageInfo;
        }

        // PATCH api/movies/5
        [HttpPatch("{id}")]
        public MessageInfo Patch(int id, [FromBody] Film value)
        {
            value.Id = id;

            MessageInfo messageInfo = new MessageInfo();
            if (string.IsNullOrEmpty(value.Title))
            {
                messageInfo.Message.Add("Title tidak boleh kosong");
            }
            else if (value.Title.Length > 255)
            {
                messageInfo.Message.Add("Maksimal panjang Title adalah 255 karakter");
            }
            if (value.Description.Length > 1000)
            {
                messageInfo.Message.Add("Maksimal panjang Description adalah 1000 karakter");
            }
            if (value.Image.Length > 255)
            {
                messageInfo.Message.Add("Maksimal panjang Image adalah 255 karakter");
            }

            if (!messageInfo.Message.Any())
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        conn.Open();
                        string sql = @"update film set title = @title, description = @description, rating = @rating, image = @image, updated_at = now() where id = @id";
                        int upd = conn.Execute(sql, value);

                        messageInfo.Status = upd > 0 ? true : false;
                        messageInfo.Message.Add(messageInfo.Status ? _msg.SUCCESS_UPDATE : _msg.DATA_NOTFOUND);
                    }
                    catch (Exception ex)
                    {
                        messageInfo.Message.Add(_msg.FAIL_UPDATE);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return messageInfo;
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public MessageInfo Delete(int id)
        {
            MessageInfo ret = new MessageInfo();
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"delete from film where id = @id";
                    int del = conn.Execute(sql, new { id = id});
                    ret.Status = del > 0 ? true : false;
                    ret.Message.Add(ret.Status ? _msg.SUCCESS_DELETE : _msg.DATA_NOTFOUND);
                }
                catch (Exception ex)
                {
                    ret.Message.Add(_msg.FAIL_DELETE);
                }
                finally
                {
                    conn.Close();
                }
            }
            return ret;
        }
    }
}
