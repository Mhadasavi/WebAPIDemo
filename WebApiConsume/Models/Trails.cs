﻿using System.ComponentModel.DataAnnotations;

namespace WebApiConsume.Models
{
    public class Trails
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public enum DifficultyType { Easy, Moderate, Difficult, Expert }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
        public NationalPark NationalPark { get; set; }
    }
}