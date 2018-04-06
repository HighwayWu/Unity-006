# Unity-006

# Unity之万丈高楼第六砖

做做一些经典的小游戏吧...就当练手了 能对UGUI加深点理解

看着感觉很简单 做起来还是会遇到一些稀奇古怪的"bug"...

比如蛇头跟屏幕边缘碰撞的时候 怎么样都无法触发OnTriggerEnter2D...

把碰撞器刚体啥的全查了一遍 最后才发现是两个collider太小了...直接互相穿过去了

还有安卓运行时如果切出去然后再回来会自动变成竖版...所以横板游戏还要记得在OnApplicationPaused里面重新SetResolution一次

WASD是上左下右 或者按右下角的按钮也行 速度会根据得分不断的增加

![image](https://github.com/HighwayWu/Unity-006/raw/master/Screenshots/图片1.png)

![image](https://github.com/HighwayWu/Unity-006/raw/master/Screenshots/图片2.png)

![image](https://github.com/HighwayWu/Unity-006/raw/master/Screenshots/图片3.png)

最后一个阶段由于我设置的速度太快了...所以把蛇改成单色的 不然颜色切换的晃眼睛...

![image](https://github.com/HighwayWu/Unity-006/raw/master/Screenshots/图片4.png)

PC版在PC文件夹下，安卓APK在AndroidAPK文件夹下。
