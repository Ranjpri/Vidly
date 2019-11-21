using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly_Auth.Models;
using Vidly_Auth.Models.Dto;

namespace Vidly_Auth.Controllers.Api
{
    public class MoviesController : ApiController
    {
        ApplicationDbContext _context;
        IMapper iMapper;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Movie, MovieDto>();
                cfg.CreateMap<MovieDto, Movie>();
            });
            iMapper = config.CreateMapper();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/movies
        public IHttpActionResult GetMovies()
        {
            var moviesDto = _context.Movies.ToList().Select(iMapper.Map<Movie, MovieDto>);
            return Ok(moviesDto);
        }

        //GET /api/movies/1
        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            MovieDto movieDto = iMapper.Map<Movie, MovieDto>(movie);
            return Ok(movieDto);
        }


        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            Movie movie = iMapper.Map<MovieDto, Movie>(movieDto);
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        //PUT /api/Movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
                return NotFound();
            iMapper.Map<MovieDto, Movie>(movieDto, movieInDb);

            _context.SaveChanges();
            return Ok(movieDto);
        }

        //Delete /api/Moves/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
