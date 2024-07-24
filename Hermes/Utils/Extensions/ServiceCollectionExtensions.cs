﻿using Hermes.Builders;
using Hermes.Models;
using Hermes.Repositories;
using Hermes.Services;
using Hermes.ViewModels;
using Hermes.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Hermes.Utils.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<Settings>();

        collection.AddSingleton<HermesContext>();
        collection.AddTransient<SfcResponseRepository>();
        collection.AddTransient<UnitUnderTestRepository>();

        collection.AddTransient<SfcService>();
        collection.AddTransient<FileService>();
        collection.AddTransient<FolderWatcherService>();
        collection.AddTransient<SfcSenderService>();
        collection.AddTransient<SfcService>();
        collection.AddTransient<StopService>();

        collection.AddSingleton<ILogger, HermesLogger>();
        collection.AddSingleton<UnitUnderTestBuilder>();

        collection.AddTransient<MainViewModel>();
        collection.AddTransient<UutProcessorViewModel>();
        collection.AddTransient<SuccessViewModel>();
        collection.AddTransient<StopViewModel>();
        collection.AddTransient<TokenViewModel>();

        collection.AddTransient<ViewManager>();
        collection.AddTransient<SuccessView>();
        collection.AddTransient<StopView>();
        collection.AddTransient<TokenView>();
    }
}