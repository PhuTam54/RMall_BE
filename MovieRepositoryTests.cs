//using AutoMapper;
//using NUnit.Framework;
//using RMall_BE.Data;

//namespace RMall_BE.Repositories.MovieRepositories.Tests
//{
//    public class MovieRepositoryTests
//    {
//        private MovieRepository _movieRepository;
//        private Mock<RMallContext> _mockContext;
//        private Mock<IMapper> _mockMapper;

//        [SetUp]
//        public void Setup()
//        {
//            // Initialize the mocks for dependencies
//            _mockContext = new Mock<RMallContext>();
//            _mockMapper = new Mock<IMapper>();

//            // Set up any necessary behavior or return values for the mocks

//            // Initialize the MovieRepository with the mocked dependencies
//            _movieRepository = new MovieRepository(_mockContext.Object, _mockMapper.Object);
//        }

//        [Test]
//        public void ConvertTimeToDouble_ShouldReturnTotalSeconds()
//        {
//            // Arrange
//            string time = "2 hrs 50 mins 10 ses";
//            double expectedTotalSeconds = 10210;

//            // Act
//            double actualTotalSeconds = _movieRepository.ConvertTimeToDouble(time);

//            // Assert
//            Assert.AreEqual(expectedTotalSeconds, actualTotalSeconds);
//        }
//    }
//}
