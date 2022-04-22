﻿using Net6WebApiTemplate.IntegrationTests.Fixtures;
using Xunit;

namespace Net6WebApiTemplate.IntegrationTests.Collections
{
    [CollectionDefinition(nameof(IntegrationTestSuiteCollection), DisableParallelization = true)]
    public class IntegrationTestSuiteCollection : ICollectionFixture<IntegrationTestSuiteFixture>
    {
    }
}
