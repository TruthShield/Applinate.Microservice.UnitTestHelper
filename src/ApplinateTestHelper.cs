// Copyright (c) TruthShield, LLC. All rights reserved.

namespace Applinate.Test
{
    using Applinate.Internals;
    using System.Collections.Immutable;

    /// <summary>
    /// Class TestHelper.
    /// </summary>
    public static class ApplinateTestHelper
    {
        /// <summary>
        /// Mocks the command for the test context.  A successful result is returned unless
        /// there is an exception thrown.
        ///
        /// It's important to understand that this context is lost once the process
        /// executing the thread executes somewhere else.
        ///
        /// For example, if the test is an integration test, this test context will
        /// no longer be in play.  For integration tests where you are hopping to
        /// another context, you have to mock the behavior globally using
        /// <see cref="MockCommandGlobally{TRequest}(Action{TRequest})"/>
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse
        public static void MockCommandForTestDuration<TArg>(Action<TArg> f)
            where TArg : class, IReturn<CommandResponse> =>
                MockCommand.SetForTestScope(f);

        /// <summary>
        /// Mocks the command globally.  This is active for all threads across all tests.
        /// This is only used when there is a separate process and the <see cref="AsyncLocal{T}"/>
        /// can't track the same thread across the unit test.
        ///
        /// For example: If you have an integration test that is calling a web service, once
        /// the service is called, the test context is no longer available and your mocks can only
        /// be hit if you set the mock globally.
        ///
        /// If you use this method, you can not run tests in parallel because they will all share
        /// this same setting for their mocked behavior.
        ///
        /// Most of the time, you'll want to use
        /// <see cref="MockCommandForTestDuration{TArg}(Action{TArg})"/>
        /// so that you can run tests in parallel.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t argument.</typeparam>
        /// <typeparam name="TResponse">The type of the t result.</typeparam>
        /// <param name="f">The f.</param>
        public static void MockCommandGlobally<TRequest>(Action<TRequest> f)
            where TRequest : class, IReturn<CommandResponse> =>
                MockRequest<TRequest, CommandResponse>.SetGlobally(r =>
                {
                    try
                    {
                        f(r); return CommandResponse.Success;
                    }
                    catch (Exception ex)
                    {
                        return CommandResponse.Failure(ex.Message);
                    }
                });

        /// <summary>
        /// Mocks the request for the test context.
        ///
        /// It's important to understand that this context is lost once the process
        /// executing the thread executes somewhere else.
        ///
        /// For example, if the test is an integration test, this test context will
        /// no longer be in play.  For integration tests where you are hopping to
        /// another context, you have to mock the behavior globally using
        /// <see cref="MockRequestGlobally{TRequest, TResponse}(Func{TRequest, TResponse})"/>
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="f"></param>
        public static void MockRequestForTestDuration<TRequest, TResponse>(Func<TRequest, TResponse> f)
        where TRequest : class, IReturn<TResponse>
        where TResponse : class, IHaveResponseStatus =>
            MockRequest<TRequest, TResponse>.SetForTestScope(f);

        /// <summary>
        /// Mocks the request globally.  This is active for all threads across all tests.
        /// This is only used when there is a separate process and the <see cref="AsyncLocal{T}"/>
        /// can't track the same thread across the unit test.
        ///
        /// For example: If you have an integration test that is calling a web service, once
        /// the service is called, the test context is no longer available and your mocks can only
        /// be hit if you set the mock globally.
        ///
        /// If you use this method, you can not run tests in parallel because they will all share
        /// this same setting for their mocked behavior.
        ///
        /// Most of the time, you'll want to use <see cref="MockRequestForTestDuration{TArg, TResult}(Func{TArg, TResult})"/>
        /// so that you can run tests in parallel.
        /// </summary>
        /// <typeparam name="TRequest">The type of the t argument.</typeparam>
        /// <typeparam name="TResponse">The type of the t result.</typeparam>
        /// <param name="f">The f.</param>
        public static void MockRequestGlobally<TRequest, TResponse>(Func<TRequest, TResponse> f)
        where TRequest : class, IReturn<TResponse>
        where TResponse : class, IHaveResponseStatus =>
            MockRequest<TRequest, TResponse>.SetGlobally(f);

        public static void OnlyLoadReferencedAssemblies(bool value = true) =>
            TypeRegistry.LoadFromDisk = !true;   

        public static void SetRequestContext(ServiceType commandType) =>
            new RequestContext(
                currentServiceType    : commandType,
                sessionId             : SequentialGuid.NewGuid(),
                conversationId        : SequentialGuid.NewGuid(),
                appContext            : AppContextKey.Empty,
                requestCallCount      : RequestContextProvider.RequestCallCount,
                decoratorCallCount    : RequestContextProvider.DecoratorCallCount,
                metadata              : RequestContextProvider.Metadata,
                userProfileId         : RequestContextProvider.UserProfileId)
            .SetCurrentRequestContext();

        public static void SetRequestContextMetadata(IDictionary<string, string> md) =>
            new RequestContext(
                currentServiceType    : RequestContextProvider.ServiceType,
                sessionId             : SequentialGuid.NewGuid(),
                conversationId        : SequentialGuid.NewGuid(),
                appContext            : AppContextKey.Empty,
                requestCallCount      : RequestContextProvider.RequestCallCount,
                decoratorCallCount    : RequestContextProvider.DecoratorCallCount,
                metadata              : md.ToImmutableDictionary(),
                userProfileId         : RequestContextProvider.UserProfileId)
            .SetCurrentRequestContext();
    }
}