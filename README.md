# xUI-Frame
A simple UI Framework with Unity3d

***使用方式

![加载失败](https://github.com/HeadsChen/xUI-Frame/raw/master/README/xUI框架.png)

可通过打开Demo中main场景体验。

***框架核心 Mediator-ContextModel-View

View负责自身显示，Mediator负责交互逻辑，从而使显示与逻辑分离。

通过View添加UI控件并控制其显示，添加数据监听及绑定按键对象。每个View会绑定一个Mediator，作为其逻辑行为的调度器，在Mediator中发送更新数据的消息及绑定按键对应的时间委托。

ContextModel 数据模型，由MessageDispatcher维护，自身值改变时会触发变更事件（View改变显示）。

数据消息和按键事件分别由MessageDispatcher和ButtonTriggerListener分发操作。

UI的层级结构由UIManager操作，在其内部维护一个TreeStack<Mediator>类型的UI树形栈。
该结构可实现某一面板（Mediator）可持有与其同级的子面板。另外可从栈顶直接返回到指定面板，而无需重复Pop。