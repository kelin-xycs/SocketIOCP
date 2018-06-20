# SocketIOCP
一个 用 C# Socket 实现 的  IOCP

这是一个 用 C# Socket 实现 的 IOCP 。

在这个 项目 里， 还实现了一个 基于 这个 IOCP 的 简单的 Web Server 的 Demo。 可以对 Get 请求 作 简单的 处理。 当然 这个处理 是 很简单的， 仅仅是 返回 “Hello World !” 这个问候。 ^^

在 我的 电脑 上 测试 的 结果 是 每秒 可以处理 约 120 个 请求 。 最高可以达到 每秒 136 个 请求 。

SocketIOCP 由 Listener 和 Worker 2 个类 来 完成 工作 。

Listener 类 负责 监听端口， Worker 类 负责 接收数据。

Listener 监听， 当有 客户端 连接上来 时， 返回 Socket 对象。 并将 Socket 对象 交给 Worker。

一个 Worker 对应 多个 Socket， 就是说， 一个 Worker 会 拥有 多个 Socket 对象， 并 轮流 对 每个 Socket 读取数据 。

一个 Worker 对应 一个 线程。 就是说， 如果 一个 Worker 拥有 10 个 Socket 的话， 那么 就会有 10 个 Socket 里 在 一个 线程 里 被 轮流读取数据。

在 Listener 中， 有 2个 参数， 目前 默认设定 如下：

private int maxSocketCountPerWorker = 50; 

rivate int maxWorkerCount = 20;

maxSocketCountPerWorker 表示 每个 Worker 最大 的 Socket 数量 

maxWorkerCount 表示 Worker 的 最大数量

上述默认设定 表示 一个 Listener 最多 能 有 20 个 Worker 同时 工作， 每个 Worker 最多 轮流 对 50 个 Socket 读取数据 。

客户端 Test 程序 会 创建 50 个 线程， 每个 线程 通过 while 循环， 不停的 发送 Request， 直到 按下 停止 按钮 。

在这样的 设定 下， 在 实际测试 中， Worker 会 被 创建 1 个 。
就是说， 来自 客户端 的 50 个 Socket 都由 1 个 Worker（线程） 在 轮流 读取数据 。

此时 的 测试表现 是， Server 进程 的 CPU 占用率 在 1% - 2% 之间 ， 偶尔会到 2.1% 。 Client Test 进程 CPU 占用率 在 22% 左右， 有时 会 达到 25% 26% 27% 。 运行稳定后， 每秒 处理 的 请求数 约在 90 - 100 个 ， 但 最高 可达到 136 个 。

有意思的是，

如果 将 设定 改为 maxSocketCountPerWorker = 40 ， 则 每秒 处理 的 请求数 是 96 个 。
Client Test 进程 CPU 占用率 在 27% 左右， 经常接近 30%， 有时 略超过 30% ，
Server 进程 的 CPU 占用率 会达到 17% 左右 ， 经常会 接近 20% ，
通过 观察 log ， 可以看到 ， 此时， 创建了 2 个 Worker， 也就是说， 有 2个 线程 在 对 Socket 读取数据 。 Worker 1 的 Socket 是 40 个， Worker 2 的 Socket 是 10 个， 加起来 是 50 个。

如果 将 设定 改为 maxSocketCountPerWorker = 10 ， 则 每秒 能 处理 120 - 130 个 请求 。 Server 进程 的 CPU 占用率 在 5% 左右，
Client Test 进程 的 CPU 占用率 在 50% 以上 。 此时， 会 创建 5 个 Worker（线程）， 每个 Worker 拥有 10 个 Socket 。

如果 将 设定 改为 maxSocketCountPerWorker = 5 ， 则 每秒 处理 的 请求数 约在 90 个 ， Server 进程 的 CPU 占用率 在 20% - 25% 之间，
Client Test 进程 CPU 占用率 在 35% 左右， 此时 会 创建 10 个 Worker（线程） ， 每个 Worker 拥有 5 个 Socket 。

注意， 以上 的 测试 是 Client 和 Server 在 同一台 电脑， 如果 放到 实际 的 网络环境 中， 服务器资源 完全 用于 Server 端， 应该 会有 更好的 表现 。 同时， 在 实际 的 网络环境 中， 网络延迟 是 一个 主要矛盾， 这也许 会 更加 发挥出 IOCP 的 优势。

IOCP 实际上 应该就是 解决 数据监听 -> 数据接收 -> 业务处理 这 3 者 之间 的 一个 平衡点 。 用 最小数量 的 线程 来 解决 数据接收 的 问题 。

我 在 SocketIOCP 的 Server 项目 中 的 IO回调 方法 ProcessBytes() 中 写了这样 的 注释：

// 实际 的 业务处理 要 放到 一个 新线程 中 处理

// IO 回调 只负责 数据接收 和 初步的 解析

// IO 回调 接收到 一个 完整的 Request 数据， 就应该把 数据 Pass 给 新线程 来 处理

// 业务处理 由 新线程 负责

IO 回调 方法， 应该负责 接收数据 和 确认 接收到 完整的 一条数据。 当 接收 到 一条 完整的 数据 ，
如 一个 Request Message 时， 就 应 将 数据 （Request Message） 转交 给 新线程 去 处理 。


















