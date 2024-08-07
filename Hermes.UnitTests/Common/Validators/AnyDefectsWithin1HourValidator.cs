using Hermes.Builders;
using Hermes.Common.Validators;
using Hermes.Models;
using Hermes.Repositories;
using Moq;

namespace HermesTests.Common.Validators;

public class AnyDefectsWithin1HourValidatorTests
{
    private readonly UnitUnderTestBuilder _unitUnderTestBuilder;

    public AnyDefectsWithin1HourValidatorTests(UnitUnderTestBuilder unitUnderTestBuilder)
    {
        this._unitUnderTestBuilder = unitUnderTestBuilder;
    }

    [Fact]
    public async Task ValidateAsync_WithAnyDefects_ReturnStop()
    {
        var defect = new Defect();
        var defectRepositoryMock = GetDefectRepositoryMock(defect);
        var unitUnderTest = _unitUnderTestBuilder.Build();

        var sut = new AnyDefectsWithin1HourValidator(defectRepositoryMock);
        Assert.False((await sut.ValidateAsync(unitUnderTest)).IsNull);
    }

    [Fact]
    public async Task ValidateAsync_NotConsecutiveDefects_ReturnStopNull()
    {
        var defectRepositoryMock = GetDefectRepositoryMock(Defect.Null);
        var unitUnderTest = _unitUnderTestBuilder.Build();

        var sut = new AnyDefectsWithin1HourValidator(defectRepositoryMock);
        Assert.True((await sut.ValidateAsync(unitUnderTest)).IsNull);
    }

    private IDefectRepository GetDefectRepositoryMock(Defect? defect = null)
    {
        var defects = new List<Defect>();
        if (defect is { IsNull: false })
        {
            defects.Add(defect);
        }

        var defectRepositoryMock = new Mock<IDefectRepository>();
        defectRepositoryMock
            .Setup(x => x.GetAnyNotRestoredDefectsWithin1Hour(It.IsAny<int>()))
            .Returns(Task.FromResult(defects));
        return defectRepositoryMock.Object;
    }
}