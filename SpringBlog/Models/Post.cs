﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpringBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        public string FeaturedImagePath { get; set; }

        [Required]
        [MaxLength(200)]
        public string Slug { get; set; }

        [Required]    //sistem otomatik milat atamasın diye req ve ? kullandık.
        public DateTime? CreationTime { get; set; }

        [Required]
        public DateTime? ModificationTime { get; set; }

        public virtual ApplicationUser Author { get; set; }
        
        public virtual Category Category { get; set; }
    }
}