using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
    public class LibraryAssetServices : ILibraryAsset
    {
        private readonly LibraryContext context;

        public LibraryAssetServices(LibraryContext context)
        {
            this.context = context;
        }
        public void Add(LibraryAsset newAsset)
        {
            context.Add(newAsset);
            context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return context.LibraryAssets
                .Include(asset=>asset.Status)
                .Include(asset=>asset.Location);

        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = context.LibraryAssets.OfType<Book>()
                .Where(asset => asset.Id == id).Any();
            var isVideo = context.LibraryAssets.OfType<Book>()
                .Where(asset => asset.Id == id).Any();
            return isBook ?
                context.Books.FirstOrDefault(book => book.Id == id).Author :
                context.Videos.FirstOrDefault(video => video.Id == id).Director
                ?? "Unknown";

        }

        public LibraryAsset GetById(int id)
        {
            return 
                GetAll()
                .FirstOrDefault(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return context.LibraryAssets.FirstOrDefault(asset => asset.Id == id).Location;
            //GetById(id).Location
        }

        public string GetDeweyIndex(int id)
        {
            if (context.Books.Any(book => book.Id == id))
            {
                return context.Books
                    .FirstOrDefault(book => book.Id == id).DeweyIndex;
            }
            else return "";
        }

        public string GetIsbn(int id)
        {
            if (context.Books.Any(a=> a.Id == id))
            {
                return context.Books
                    .FirstOrDefault(a => a.Id == id).ISBN;
            }
            else return "";
        }

        public string GetTitle(int id)
        {
            return context.Books
                   .FirstOrDefault(a => a.Id == id)
                   .Title;
        }

        public string GetType(int id)
        {
            var book = context.LibraryAssets.OfType<Book>().
                Where(b => b.Id == id);
            return book.Any() ? "Book" : "Video";
        }
    }
}
