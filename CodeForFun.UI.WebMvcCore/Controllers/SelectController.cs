using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeForFun.Core.Entities;
using CodeForFun.Repository.DataAccess.Abstract;
using CodeForFun.Repository.DataAccess.DbContexts;
using CodeForFun.UI.WebMvcCore.Data;
using CodeForFun.UI.WebMvcCore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeForFun.UI.WebMvcCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SelectController : ControllerBase
	{
		private readonly IRepositoryWrapper _repository;
		private readonly RepositoryContext _db;
		string nameSpace = "CodeForFun.Repository.Entities.Concrete.";

		public SelectController(IRepositoryWrapper repository)
		{
			_repository = repository;
			_db = _db ?? new RepositoryContext();

		}

		[HttpPost, DisableRequestSizeLimit]
		[Route("SearchInFiles")]
		public ActionResult SearchInFiles()
		{
			IList<SearchFile> filesArr = new List<SearchFile>();

			var files = Request.Form.Files;
			foreach (var file in files)
			{
				string fileType = System.IO.Path.GetExtension(file.FileName).Replace(".", "").ToString();
				switch (fileType)
				{
					case "txt":
						using (var p = file.OpenReadStream())
						{
							byte[] array = new byte[p.Length];
							p.Read(array, 0, array.Length);
							filesArr.Add(new SearchFile {
								FileName = file.Name,
								FilePath = file.FileName,
								Value = (System.Text.Encoding.Default.GetString(array))
							});

						}
						break;

					default:
						filesArr.Add(new SearchFile
						{
							FileName = file.Name,
							FilePath = file.FileName,
						});
						break;
				}
						

			}

			return Ok(filesArr);
		}

		[HttpGet]
		[Route("GetAll")]
		public IEnumerable<string> Get()
		{
			return _repository.GetType().GetProperties().Select(x => x.Name);
		}

		[HttpGet]
		[Route("GetProperties")]
		public SearchViewModel Get(string name)
		{
			IList<string> props = new List<string>();
			var entities = _db.Query(nameSpace + name);
			var p = entities.ElementType.GetProperties().ToList();

			foreach (var prop in p)
			{
				if (prop.PropertyType.Namespace == "System")
					props.Add(prop.Name);
			}

			var searchEntity = new SearchViewModel
			{
				EntityName = name,
				Entities = entities,
				Properties = props
			};
			return searchEntity;
		}
	}
}