﻿using System;
using Funq;
using ServiceStack.Common.Utils;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using ServiceStack.WebHost.Endpoints;

namespace ServiceStack.MovieRest
{
	public class AppHost
		: AppHostBase
	{
		public AppHost()
			: base("ServiceStack REST at the Movies!", typeof(MovieService).Assembly) {}

		public override void Configure(Container container)
		{
			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(
					"~/App_Data/db.sqlite".MapHostAbsolutePath(),
					SqliteOrmLiteDialectProvider.Instance));

			var resetMovies = container.Resolve<ResetMoviesService>();
			resetMovies.Post(null);

			Routes
			  .Add<Movie>("/movies", "POST,PUT")
			  .Add<Movie>("/movies/{Id}")
			  .Add<Movies>("/movies")
			  .Add<Movies>("/movies/genres/{Genre}");
		}
	}

	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			new AppHost().Init();
		}
	}
}