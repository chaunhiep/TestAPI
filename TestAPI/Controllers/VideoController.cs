using TestAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Enum;
using Microsoft.AspNetCore.Authorization;

namespace TestAPI.Controllers
{
    [Log]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private static readonly List<Video> Videos = new()
        {
            new Video()
            {
                Id = 1,
                Source = "./video/SpiderMan-2.mp4",
                Image = "./image/spider-verse.png",
                Title = "Spider-Man: Into the Spider-Verse",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Trailer,
            },
            new Video()
            {
                Id = 2,
                Source = "./video/LastKingdom.mp4",
                Image = "./image/last-kingdom.jpg",
                Title = "The Last Kingdom: Seven Kings Must Die",
                Description = "As Alfred the Great defends his kingdom from Norse invaders, Uhtred--born a Saxon but raised by Vikings--seeks to claim his ancestral birthright.",
                Tag = VideoType.Trailer,
            },
            new Video()
            {
                Id = 3,
                Source = "./video/SpiderMan-2.mp4",
                Image = "./image/spider-man.jpg",
                Title = "Spider-Man: Across the Spider-Verse",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Trailer,
            },
            new Video()
            {
                Id = 4,
                Source = "./video/TheFlash.mp4",
                Image = "./image/the-flash.jpg",
                Title = "The Flash (2023)",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Trailer,
            },

            new Video()
            {
                Id = 5,
                Source = "./video/Elemental.mp4",
                Image = "./image/elemental.jpg",
                Title = "Elemental",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Intro,
            },
            new Video()
            {
                Id = 6,
                Source = "./video/Avatar.mp4",
                Image = "./image/avatar.jpg",
                Title = "Avatar (2022)",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Intro,
            },
            new Video()
            {
                Id = 7,
                Source = "./video/LastKingdom.mp4",
                Image = "./image/last-kingdom.jpg",
                Title = "The Last Kingdom",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Intro,
            },
            new Video()
            {
                Id = 8,
                Source = "./video/Tar.mp4",
                Image = "./image/tar-movie.jpg",
                Title = "Tár",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Intro,
            },
            new Video()
            {
                Id = 9,
                Source = "./video/SpiderMan-2.mp4",
                Image = "./image/spider-man.jpg",
                Title = "Spider-Man: Into the Spider-Verse",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Intro,
            },
            new Video()
            {
                Id = 10,
                Source = "./video/TheFlash.mp4",
                Image = "./image/the-flash.jpg",
                Title = "The Flash",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Intro,
            },

            new Video()
            {
                Id = 11,
                Source = "./video/LastKingdom.mp4",
                Image = "./image/last-kingdom.jpg",
                Title = "The Last Kingdom",
                Description = "Miles Morales catapults across the Multiverse, where he encounters a team of Spider-People charged with protecting its very existence. When the heroes clash on how to handle a new threat, Miles must redefine what it means to be a hero.",
                Tag = VideoType.Suggest,
            },
            new Video()
            {
                Id = 12,
                Source = "./video/Tar.mp4",
                Image = "./image/tar-movie.jpg",
                Title = "Tár",
                Description = "Renowned musician Lydia Tár is days away from recording the symphony that will elevate her career. When all elements seem to conspire against her, Lydia's adopted daughter Petra becomes an integral emotional support for her struggling mother.",
                Tag = VideoType.Suggest,
            },
            new Video()
            {
                Id = 13,
                Source = "./video/Elemental.mp4",
                Image = "./image/elemental.jpg",
                Title = "Elemental",
                Description = "Renowned musician Lydia Tár is days away from recording the symphony that will elevate her career. When all elements seem to conspire against her, Lydia's adopted daughter Petra becomes an integral emotional support for her struggling mother.",
                Tag = VideoType.Suggest,
            },
            new Video()
            {
                Id = 14,
                Source = "./video/Interstellar.mp4",
                Image = "./image/interstellar.jpg",
                Title = "Interstellar",
                Description = "Renowned musician Lydia Tár is days away from recording the symphony that will elevate her career. When all elements seem to conspire against her, Lydia's adopted daughter Petra becomes an integral emotional support for her struggling mother.",
                Tag = VideoType.Suggest,
            }
        };

        private readonly ILogger<VideoController> _logger;

        public VideoController(ILogger<VideoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("api/videos")]
        [AllowAnonymous]
        public IEnumerable<Video> GetAll()
        {
            return Videos.ToList();
        }

        [HttpGet]
        [Route("api/videos/{id}")]
        [CustomAuthorize]
        public Video? GetById(int id)
        {
            Console.WriteLine("So sadddd");
            return Videos.FirstOrDefault(c => c.Id == id);
        }

        [HttpPost]
        [Route("api/searchfull/videos")]
        [CustomAuthorize]
        public IEnumerable<Video> SearchAsUser(SearchRequest request)
        {
            return Videos.Where(c => c.Title != null && request.Title != null 
                && c.Title.ToLower().Contains(request.Title.ToLower())).ToList();
        }

        [HttpPost]
        [Route("api/search/videos")]
        [AllowAnonymous]
        public IEnumerable<Video> SearchAsGuest(SearchRequest request)
        {
            return Videos.Where(c => c.Tag != VideoType.Suggest && c.Title != null 
                && request.Title != null && c.Title.ToLower().Contains(request.Title.ToLower())).ToList();
        }
    }
}