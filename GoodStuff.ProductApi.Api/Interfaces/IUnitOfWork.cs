namespace GoodStuff.ProductApi.Application.Interfaces;

public interface IUnitOfWork
{
    ICpuRepository CpuRepository { get; set; }
    IGpuRepository GpuRepository { get; set; }
    ICoolerRepository CoolerRepository { get; set; }
}