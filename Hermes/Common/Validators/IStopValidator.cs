using Hermes.Models;
using System.Threading.Tasks;

namespace Hermes.Common.Validators;

public interface IStopValidator : IValidator<SfcResponse, Task<Stop>>
{
}