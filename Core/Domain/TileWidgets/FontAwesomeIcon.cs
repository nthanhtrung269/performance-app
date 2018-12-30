﻿using System;

namespace Core.Domain.TileWidgets
{
    public class FontAwesomeIcon : ValueObject<FontAwesomeIcon>
    {
        public static string StickyNote = "fa fa-sticky-note";
        public static string Cogs = "fa fa-cogs";
        public static string LargeBook = "fa fa-4x fa-book";
        public static string LargeLink = "fa fa-4x fa-link";
        public static string LargeBriefCase = "fa fa-4x fa-briefcase";
        
        public string Name { get; set; }

        public static FontAwesomeIcon Build(string name)
        {
            return new FontAwesomeIcon(name);
        }

        protected FontAwesomeIcon()
        {
        }

        protected FontAwesomeIcon(string name)
        {
            Name = name;
        }

        protected override bool EqualsCore(FontAwesomeIcon comparedIcon)
        {
            if (comparedIcon.Name == Name)
            {
                return true;
            }
            return false;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Convert.ToInt32(Name);
                hashCode = (hashCode * 397) ^ Convert.ToInt32(Name);
                return hashCode;
            }
        }
    }
}
