# SquareWE
2023 Tsinghua University Electronic Engineering Software Designing Competition



## 文件夹说明

* Assets文件夹存储所有游戏资源，**请务必将资源文件按照如下进行分类并在每一个下方添加必要文件说明，比如对SampleScene的说明**
  * meta文件绝对不能自行修改或者删除
  * Background文件夹用来存储背景图片
    * ……
  * Music文件夹用来存储音乐文件
    * ……
  * Logic文件夹用来存储游戏逻辑对应的C#文件
    * ……
  * Material文件夹用来存储材质文件
    * ……
  * Pictures文件夹用来存储除背景图片之外的其它图片
    * ……
  * Scripts文件夹用来存储脚本文件
    * ……
  * Scenes文件夹用来存储游戏场景
    * SampleScene: 示例场景
  * 如果发现分类不足，可以自行加入文件夹，并在此处进行说明
  * ……

## github说明

* 建议先将远程仓库fork到自己的仓库，然后再clone到本地，或者直接从sendssf仓库克隆也行
* 仓库的dev分支所有人都可以直接合并，但是main分支只能先提PR，需要有两个人点了approval以后才能合并。
* 如果进行了Unity操作，提交之前先点一下运行，确认资源导入无误后进行提交。

## 开发工具

* Visual Studio 2022（在安装时需要选择".NET桌面开发","使用Unity的游戏开发" ，**占用空间很大，建议别装C盘**）
* Unity2021.3.7f1(**最起码必须是2021**)

### 风格说明

几项重要规定如下：

- 需严格按照要求缩进

  ```C#
  namespace Exp
  {
      public class Program
      {
          public static void Main()
          {            
          }
      }
  }
  ```

- `if`、`while`等关键字后须加空格

  ```C#
  while (1)
  {
      // code
  }
  ```

- 大括号须换行书写

  ```C#
  // Allowed
  if (...)
  {
     // ... 
  }
  
  // Forbidden !!!
  if (...) {
      // ...
  }
  ```

* 为代码添加必要的注释
* 变量命名不宜过长但是含义要清晰
