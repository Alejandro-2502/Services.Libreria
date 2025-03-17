using AutoMapper;
using Libreria.Application.Gateways;
using Libreria.Application.Interactors;
using Libreria.Application.Interfaces.ICommon;
using Libreria.Application.Responses;
using Moq;
using System.ComponentModel;
using System.Net;

namespace Testing.Libreria.Libro.Querys;

[TestClass]
public class QuerysTest
{
    private readonly Mock<ILibroQuerysGateway> _mockLibroQuerysGateway;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogServicesInteractor> _mockLogServicesInteractor;
    public QuerysTest()
    {
        _mockLibroQuerysGateway = new Mock<ILibroQuerysGateway>();
        _mockMapper = new Mock<IMapper>();
        _mockLogServicesInteractor = new Mock<ILogServicesInteractor>();
    }

    [TestMethod]
    [Category("GetAll - OK")]
    public void TestGetAllOk()
    {
        var _libroQuerysInteractor = new LibroQuerysInteractor(_mockLogServicesInteractor.Object,
                                                                _mockLibroQuerysGateway.Object,
                                                                _mockMapper.Object);

        var LibrosDto = QuerysData.GetLibroDTO();
        var LibrosResponse = QuerysData.GetLibroResponse();

        _mockLibroQuerysGateway.Setup(x => x.GetAll())
                .ReturnsAsync(LibrosDto);

        _mockMapper.Setup(map => map.Map<List<LibroResponse>>(LibrosDto))
            .Returns(LibrosResponse);

        var response = _libroQuerysInteractor.GetAll().Result;


        Assert.IsTrue(response.Success);
        Assert.IsTrue(response.Messages.Count() > 0);
        Assert.IsNotNull(response.Response);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    }

    [TestMethod]
    [Category("GetAll - NotFound")]
    public void TestGetAllNotFound()
    {
        var _libroQuerysInteractor = new LibroQuerysInteractor(_mockLogServicesInteractor.Object,
                                                                _mockLibroQuerysGateway.Object,
                                                                _mockMapper.Object);

        var LibrosDto = QuerysData.GetLibrosDTONotFound();
        var LibrosResponse = QuerysData.GetLibrosResponseNotFound();

        _mockLibroQuerysGateway.Setup(x => x.GetAll())
                .ReturnsAsync(LibrosDto);

        _mockMapper.Setup(map => map.Map<List<LibroResponse>>(LibrosDto))
            .Returns(LibrosResponse);

        var response = _libroQuerysInteractor.GetAll().Result;


        Assert.IsFalse(response.Success);
        Assert.IsTrue(response.Messages.Count() > 0);
        Assert.IsNull(response.Response);
        Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
    }

    [TestMethod]
    [Category("GetAll - InternalServerError")]
    public void TestGetAllInternalServerError()
    {
        var _libroQuerysInteractor = new LibroQuerysInteractor(_mockLogServicesInteractor.Object,
                                                                _mockLibroQuerysGateway.Object,
                                                                _mockMapper.Object);

        _mockLibroQuerysGateway.Setup(x => x.GetAll())
            .ThrowsAsync(new Exception("Error en la Base de Datos"));

        var response = _libroQuerysInteractor.GetAll().Result;
        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);

        _mockLogServicesInteractor.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
    }
}
