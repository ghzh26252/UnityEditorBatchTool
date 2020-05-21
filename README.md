# UnityEditorBatchTool
自用UnityEditor批量重命名/整列/属性设置等小工具

BackupStatic:

备份和还原场景内物体的静态标签，通过txt文本存储设有静态标签物体的InstanceID和StaticEditorFlags，用于修改后还原。

BatchArray:

阵列复制
支持XYZ三个方向
![Image](https://github.com/ghzh26252/UnityEditorBatchTool/blob/master/Image/%E9%98%B5%E5%88%97.png)

BatchSetSingleRotation:

批量设置物体的单轴LocalRotition值。
Position和Scale都可以在选择多个物体的情况下设计XYZ任意一个轴的值，而不影响其他两个轴。
但是Rotaion不可以，所以通过这个工具实现。
![Image](https://github.com/ghzh26252/UnityEditorBatchTool/blob/master/Image/%E5%8D%95%E8%BD%B4rotation.png)

PasteBake:
复制烘焙物体
烘焙过的物体使用Ctrl+Shift+D进行复制，可以保留LightMap信息。

PlayerSettingAndBuild
当一个工程需要分不同关卡Build多次时使用，保存发布名称和发布场景，下一次发布可以一键完成。
目前只按顺序保存了关卡的开关，当关卡数量发生变化时会出错，关卡顺序发生变化时请重新保存，下一步将通过保存关卡的GUID避免这些问题
![Image](https://github.com/ghzh26252/UnityEditorBatchTool/blob/master/Image/%E5%A4%9A%E5%9C%BA%E6%99%AF%E5%8F%91%E5%B8%83.png)

RemoveCollider
移除选择物体及子物体的所有Collider碰撞体。

ReNameAssets & ReNameGameObject
重命名资源 重命名场景物体
可批量改名选择物体或资源，有增加/删除/查找替换/添加序列4个功能。

ReplaceAssetReference
替换资源引用
通过修改.unity场景文件文本内的GUID，暴力替换掉该场景所引用的所有被替换资源，如材质/贴图等。
![Image](https://github.com/ghzh26252/UnityEditorBatchTool/blob/master/Image/%E6%9B%BF%E6%8D%A2%E8%B5%84%E6%BA%90.png)

ReplaceMaterial
替换材质
遍历选择物体，替换掉所有指定材质的引用。
![Image](https://github.com/ghzh26252/UnityEditorBatchTool/blob/master/Image/%E6%9B%BF%E6%8D%A2%E6%9D%90%E8%B4%A8.png)
