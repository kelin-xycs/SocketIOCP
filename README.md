# SocketIOCP
一个 用 C# Socket 实现 的  IOCP

这是一个 用 C# Socket 实现 的 IOCP 。


采用  Listener + Dispatch + Worker  的 设计， Listener 是一个线程， Dispatch 是一个线程， Worker 是 一组线程 。
一个 Socket 对应 一个 Worker ， 一个 Worker 对应一个线程 。


Listener 负责监听 Accept() ， 将 Socket 放到 SocketQueue 中，  Dispatch 负责 从 SocketQueue 中 取出 Socket ， 
为 Socket 创建一个 Worker， 并 启动 Worker 。


 
测试结果， 可以 达到  每秒 15000 个请求， 和  常规写法 ReceiveAsync()  是 一个水平 。
两者 的  CPU 占用率 也 差不多 ：  Server 进程 20% 左右，  System 进程 20% 左右 ， 测试工具  12%  左右 。


但是， 效率 最好的， 是  常规写法  Receive()，  就是  1 线程监听  Accept()，   n 线程 Receive()  ，

每秒请求 也是  15000  的 水平 ， 但是 短时间内 可以 上升 到  2 万 或者 23000  。


而且，有一点 重要 的 是，  CPU 占用率  更低 。   Server 进程 10% 以下， 很多时候 5% 左右 ，   有时候 5%  以下  。  
 System 进程  30% - 40% ， 有时候  超过 40%  。   测试工具 10% 左右 。

从这里 可以 看出来，  常规写法  Receive()  的   loading 主要 是 在  System 进程  上 。
我想 Socket 的 工作线程 就在  System 进程 里。



System 进程 是 系统进程 。 操作系统 的 内核工作 应该 就是在 这个 System 进程 里 。 包括 Socket 虚拟内存  等 。


从这里可以 看出来， 并发测试 的时候，  System 进程 正在做 跟我们 一样 的事， SocketIOCP 相当于 在 外面 又 把 Socket 做的 事  又 做了一遍 。 ^^   这就好像 在 裤子外面 又 套了一条裤子 ， 当然  会 显得 笨重 和 更加占用 CPU 资源。 

但是 反过来， 如果 从 这个角度 来看的话，   SocketIOCP 能做到 现在这样， 也是 不错 了 。 ^^

SocketIOCP 的 设计， 如果用来 处理 底层 数据 的 输入输出， 也会 表现的 很出色 的 。


最终 的 这个  版本， 实际上 和  常规写法 Receive() 一样， 为 每个 Socket 分配 一个 线程 。
这和我最初 的 “用 最小数量  的 线程 来 完成  数据处理  工作”  的 想法 不太一样 。

因为 ，  对于  分时 多任务 ，  线程 是 最自然 的 做法 。  另一方面 ， 为了 保持 实时 响应性  ，  也 只能 这样 做  。


所以， 如果 我们 要写  Socket 网络应用 ， 那么 可能 常规写法 Receive()  还是 最好 的 写法 。
常规写法 Receive()  参考 这篇文章 ：   https://blog.csdn.net/andrew_wx/article/details/6629721/           





































