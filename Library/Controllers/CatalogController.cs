﻿using Library.Models.Catalog;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class CatalogController: Controller
    {
        private readonly ILibraryAsset assets;

        public CatalogController(ILibraryAsset assets)
        {
            this.assets = assets;
        }
        public IActionResult Index()
        {
            var assetModels = assets.GetAll();
            var listingResult = assetModels
                .Select(result => new AssetIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl=result.ImageUrl,
                    AuthorOrDirector=assets.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber=assets.GetDeweyIndex(result.Id),
                    Title=result.Title,
                    Type=assets.GetType(result.Id)
            } );
            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };
        return View(model);

       }
        public IActionResult Detail(int id)
        {
            var asset = assets.GetById(id);
            var model = new AssetDetailModel
            {
                AssetId = id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = assets.GetAuthorOrDirector(id),
                CurrentLocation = assets.GetCurrentLocation(id).Name,
                DeweyCallNumber = assets.GetDeweyIndex(id),
                ISBN = assets.GetIsbn(id)
            };
            return View(model);

        }
    }
}
