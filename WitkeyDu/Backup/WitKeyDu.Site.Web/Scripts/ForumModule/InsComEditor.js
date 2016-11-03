var editor = new wangEditor('CommentContent');
// 自定义菜单
editor.config.menus = [
         'bold', 'underline', 'italic', 'strikethrough',
        'eraser', 'forecolor', 'bgcolor', 'quote', 'fontfamily',
        'fontsize', 'head', 'unorderlist', 'orderlist', 'alignleft',
         'aligncenter', 'alignright', 'link', 'unlink', 'table',
        'emotion', 'img', 'location', 'insertcode', 'undo',
         'redo', 'fullscreen'
    ];
// 颜色
editor.config.colors = {
    '#880000': '暗红色',
    '#800080': '紫色',
    '#ff0000': '红色'
};

// 字体
editor.config.familys = [
        '宋体', '黑体', '楷体', '微软雅黑',
        'Arial', 'Verdana', 'Georgia'
        ];

// 字号
editor.config.fontsizes = {
    // 格式：'value': 'title'
    1: '10px',
    2: '13px',
    3: '16px',
    4: '19px',
    5: '22px',
    6: '25px',
    7: '28px'
};
// 表情
editor.config.emotions = {
    'default': {
        title: '默认',  // 分组的标题
        size: 18,  // 表情图标的尺寸
        imgs: [
        // 每个表情图标的url地址
                '../Content/RefrenceFile/wangEditor/static/emotions/default/1.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/2.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/3.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/4.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/5.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/6.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/7.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/8.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/9.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/10.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/11.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/12.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/13.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/14.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/15.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/16.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/17.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/18.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/19.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/20.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/21.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/22.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/23.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/default/24.gif'
            ]
    },
    'jinxing': {
        title: '金星',  // 分组的标题
        size: 50,  // 表情图标的尺寸
        imgs: [
        // 每个表情图标的url地址
                '../Content/RefrenceFile/wangEditor/static/emotions/jinxing/1.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/jinxing/2.gif',
                '../Content/RefrenceFile/wangEditor/static/emotions/jinxing/3.gif'
            ]
    }
};

// 将全屏时z-index修改为20000
editor.config.zindex = 20000;
// 阻止输出log
wangEditor.config.printLog = true;
// 关闭js过滤
editor.config.jsFilter = true;
// 取消粘贴过滤
editor.config.pasteFilter = true;
// 上传图片
editor.config.uploadImgUrl = '../Content/RefrenceFile/wangEditor/dist/upload.ashx';
editor.create();