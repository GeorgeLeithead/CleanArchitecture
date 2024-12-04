﻿global using System.Reflection;
global using System.Text.Json.Serialization;
global using Application;
global using Application.Abstractions.Behaviors;
global using Application.Todos.Complete;
global using Application.Todos.Create;
global using Application.Todos.Delete;
global using Application.Todos.GetAll;
global using Application.Todos.GetById;
global using Application.Users.GetById;
global using Application.Users.Login;
global using Application.Users.Register;
global using Asp.Versioning;
global using Asp.Versioning.Builder;
global using Domain.Todos;
global using Infrastructure;
global using Infrastructure.Database;
global using MediatR;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.OpenApi;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Primitives;
global using Microsoft.OpenApi.Models;
global using Serilog;
global using Serilog.Context;
global using ServiceDefaults;
global using SharedKernel;
global using Web.Api;
global using Web.Api.Endpoints;
global using Web.Api.Extensions;
global using Web.Api.Infrastructure;
global using Web.Api.Middleware;
