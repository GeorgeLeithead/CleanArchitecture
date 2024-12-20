﻿global using System.Net;
global using System.Net.Mail;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using Application.Abstractions.Authentication;
global using Application.Abstractions.Data;
global using Application.ConfigurationOptions;
global using Application.Services;
global using Domain.Users;
global using Infrastructure.Authentication;
global using Infrastructure.Authorization;
global using Infrastructure.Database;
global using Infrastructure.Time;
global using MediatR;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity.UI.Services;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.Extensions.DependencyInjection;
global using SharedKernel;
