using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.ServiceContractLines.Queries
{
    public class GetServiceContractLineCollection : IRequest<ServiceContractLineCollectionViewModel>
    {
        
    }
}