using GoodStuff.ProductApi.Api.Interfaces;

namespace GoodStuff.ProductApi.Api.Repositories;

public class UnitOfWork(ICpuRepository cpuRepository, IGpuRepository gpuRepository, ICoolerRepository coolerRepository) : IUnitOfWork
{
    public ICpuRepository CpuRepository { get; set; } = cpuRepository;
    public IGpuRepository GpuRepository { get; set; } = gpuRepository;
    public ICoolerRepository CoolerRepository { get; set; } = coolerRepository;
}