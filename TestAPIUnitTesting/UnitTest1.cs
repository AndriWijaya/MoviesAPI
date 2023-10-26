using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using TestAPI.Controllers;
using TestAPI.Model;
using Xunit;
using TestAPI.Helper;

namespace TestAPIUnitTesting
{
    public class UnitTest1
    {
        private MoviesController moviesController;
        private AllMessage _msg;
        public UnitTest1()
        {
            moviesController = new MoviesController();
            _msg = new AllMessage();
        }

        [Fact]
        public void GetAllTest()
        {
            var result = moviesController.Get();

            Assert.IsType<ActionResult<List<Film>>>(result);
        }

        [Theory]
        [InlineData(0)]
        public void GetMovieByIdTest(int id)
        {
            var movie = moviesController.Get(id);
            if (movie.Result == null)
            {
                Assert.Null(movie.Result);
            }
            else
            {
                Assert.IsType<Film>(movie.Value);
                Assert.Equal(id, movie.Value.Id);
            }
        }

        [Fact]
        public void AddMovieTest()
        {
            //Success Request
            var movie = new Film()
            {
                Title = "Title",
                Description = "Description",
                Rating = 10,
                Image = "https://fastly.picsum.photos/id/401/536/354.jpg?hmac=klYND-Hud0ew2KcwY00pvGKab0He7yTlDBfKXOvIH3g",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            var resp = moviesController.Post(movie);

            Assert.IsType<MessageInfo>(resp);
            Assert.True(resp.Status);
            Assert.Contains(_msg.SUCCESS_INSERT, resp.Message);


            //Fail Request (no title)

            var movieFail = new Film()
            {
                Description = "Description",
                Rating = 10,
                Image = "https://fastly.picsum.photos/id/401/536/354.jpg?hmac=klYND-Hud0ew2KcwY00pvGKab0He7yTlDBfKXOvIH3g",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            var respFail = moviesController.Post(movieFail);

            Assert.IsType<MessageInfo>(respFail);
            Assert.False(respFail.Status);
        }

        [Theory]
        [InlineData(2)]
        public void UpdateMovieTest(int id)
        {
            //Success Request
            var movie = new Film()
            {
                Id = id,
                Title = "Title Edited Unit testing",
                Description = "Description Edited Unit testing",
                Rating = 2,
                Image = "https://fastly.picsum.photos/id/401/536/354.jpg?hmac=klYND-Hud0ew2KcwY00pvGKab0He7yTlDBfKXOvIH3g",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };

            var resp = moviesController.Patch(id, movie);

            Assert.IsType<MessageInfo>(resp);
            Assert.True(resp.Status);
            Assert.Contains(_msg.SUCCESS_UPDATE, resp.Message);


            //Fail Request (no title)
            var movieFail = new Film()
            {
                Description = "Description Edited Unit testing",
                Rating = 3,
                Image = "https://fastly.picsum.photos/id/401/536/354.jpg?hmac=klYND-Hud0ew2KcwY00pvGKab0He7yTlDBfKXOvIH3g",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            };
            var respFail = moviesController.Patch(id, movieFail);

            Assert.IsType<MessageInfo>(respFail);
            Assert.False(respFail.Status);
        }

        [Theory]
        [InlineData(14)]
        public void DeleteMovieByIdTest(int id)
        {
            //success test (if id exists)
            var movie = moviesController.Delete(id);
            Assert.IsType<MessageInfo>(movie);
            Assert.True(movie.Status);
            Assert.Contains(_msg.SUCCESS_DELETE, movie.Message);

            //fail test (cause data already deleted)
            movie = moviesController.Delete(id);
            Assert.IsType<MessageInfo>(movie);
            Assert.False(movie.Status);
            Assert.Contains(_msg.DATA_NOTFOUND, movie.Message);
        }
    }
}
