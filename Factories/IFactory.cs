using TheWall.Models;
using System.Collections.Generic;
namespace TheWall.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}