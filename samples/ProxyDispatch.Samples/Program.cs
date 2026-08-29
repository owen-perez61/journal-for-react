using ProxyDispatch;

var yourClass = AspectDispatchProxy.Decorate<IYourClass, YourClassProxy>(new YourClass());

yourClass.SyncMethod("百小僧");
await yourClass.AsyncMethod();
await yourClass.AsyncMethodWithResult(100);
Console.WriteLine("泛型方法 GenericMethod 调用，返回值：" + yourClass.GenericMethod("Furion"));

var yourClassNoInterface = AspectDispatchProxy.DecorateClass<YourClass, YourClassProxy>(new YourClass());

yourClassNoInterface.SyncMethod("百小僧");
await yourClassNoInterface.AsyncMethod();
await yourClassNoInterface.AsyncMethodWithResult(100);
Console.WriteLine("泛型方法 GenericMethod 调用，返回值：" + yourClassNoInterface.GenericMethod("Furion"));

Console.ReadKey();

public class YourClass : IYourClass
{
    // 同步方法
    public string SyncMethod(string str)
    {
        return str;
    }

    // 异步方法
    public async Task AsyncMethod()
    {
        await Task.CompletedTask;
    }

    // 异步方法带返回值
    public async Task<int> AsyncMethodWithResult(int num)
    {
        await Task.Delay(10);
        return num + 100;
    }

    // 泛型方法
    public T GenericMethod<T>(T x)
    {
        return x;
    }
}

public interface IYourClass
{
    // 同步方法
    string SyncMethod(string str);

    // 异步方法
    Task AsyncMethod();

    // 异步方法带返回值
    Task<int> AsyncMethodWithResult(int num);

    // 泛型方法
    T GenericMethod<T>(T x);
}

public class YourClassProxy : AspectDispatchProxy
{
    // 拦截同步
    public override object Invoke(Invocation invocation)
    {
        Console.WriteLine($"~~~调用同步方法：{invocation.Method}");
        var result = invocation.Proceed();
        Console.WriteLine($"~~~调用同步方法结束，返回值：{result}");
        return result;
    }

    // 拦截异步
    public override async Task InvokeAsync(Invocation invocation)
    {
        Console.WriteLine($"~~~调用异步 Task 方法：{invocation.Method}");
        await invocation.ProceedAsync();
        Console.WriteLine("~~~调用异步 Task 方法结束");
    }

    // 拦截异步带返回值
    public override async Task<T> InvokeAsync<T>(Invocation invocation)
    {
        Console.WriteLine($"~~~调用异步 Task<T> 方法：{invocation.Method}");
        var result = await invocation.ProceedAsync<T>();
        Console.WriteLine($"~~~调用异步 Task<T> 方法结束，返回值：{result}");
        return result;
    }
}
// 885628
