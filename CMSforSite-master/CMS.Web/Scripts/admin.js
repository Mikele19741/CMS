$(function () {

  var JustBlog = {};
  
  JustBlog.GridManager = {};

  //************************* POSTS GRID
  JustBlog.GridManager.postsGrid = function (gridName, pagerName) {

    //*** Event handlers
    var afterclickPgButtons = function (whichbutton, formid, rowid) {
      tinyMCE.get("Header").setContent(formid[0]["Header"].value);
      tinyMCE.get("Body").setContent(formid[0]["Body"].value);
    };

    var afterShowForm = function (form) {
        tinyMCE.execCommand('mceAddControl', false, "Header");
      tinyMCE.execCommand('mceAddControl', false, "Body");
    };

    var onClose = function (form) {
      tinyMCE.execCommand('mceRemoveControl', false, "Header");
      tinyMCE.execCommand('mceRemoveControl', false, "Body");
    };

    var beforeSubmitHandler = function (postdata, form) {
      var selRowData = $(gridName).getRowData($(gridName).getGridParam('selrow'));
      if (selRowData["CreatedOn"])
          postdata.PostedOn = selRowData["CreatedOn"];
      postdata.Header = tinyMCE.get("Header").getContent();
      postdata.Body = tinyMCE.get("Body").getContent();

      return [true];
    };

    var colNames = [
			'id',
            'Title',
            'Meta',
			'Header',
			'Body',
			'CategoryId',
			'Category',
			'UrlSlug',
			'CreatedOn',
        'Published',
        'AlbumId',
        'Album'
       

            
    ];

    var columns = [];

    columns.push({
        name: 'id',
        index: 'id',
        width: 200,
        hidden: true,
        sortable: false,
        editable: true,
        editoptions: {
            size: 43,
            maxlength: 200
        },
        editrules: {
            required: false
        }
    });
    columns.push({
        name: 'Title',
        index: 'Title',
        width: 250,
        editable: true,
        editoptions: {
            size: 43,
            maxlength: 500
        },
        editrules: {
            required: true
        },
        formatter: 'showlink',
        formatoptions: {
            target: "_new",
            baseLinkUrl: '/Admin/GoToPost'
        }
    });
      //////
      columns.push({
          name: 'Meta',
          index: 'Meta',
          width: 250,
          sortable: false,
          editable: true,
          edittype: 'textarea',
          editoptions: {
              rows: "2",
              cols: "40",
              maxlength: 1000
          },
          editrules: {
              required: true
          }
      });

    columns.push({
        name: 'Header',
        index: 'Header',
        width: 250,
        editable: true,
        sortable: false,
        hidden: true,
        edittype: 'textarea',
        editoptions: {
            rows: "10",
            cols: "100"
        },
        editrules: {
            custom: true,
            custom_func: function (val, colname) {
                val = tinyMCE.get("Header").getContent();
                if (val) return [true, ""];
                return [false, colname + ": Field is required"];
            },
            edithidden: true
        }
    });
    columns.push({
        name: 'Body',
        index: 'Body',
        width: 250,
        editable: true,
        sortable: false,
        hidden: true,
        edittype: 'textarea',
        editoptions: {
            rows: "40",
            cols: "100"
        },
        editrules: {
            custom: true,
            custom_func: function (val, colname) {
                val = tinyMCE.get("Body").getContent();
                if (val) return [true, ""];
                return [false, colname + ": Field is requred"];
            },
            edithidden: true
        }
    });

    columns.push({
        name: 'Category.Id',
        index: 'CategoryId',
        hidden: true,
        editable: true,
        edittype: 'select',
        editoptions: {
            style: 'width:250px;',
            dataUrl: '/Admin/GetCategoriesHtml'
        },
        editrules: {
            required: true,
            edithidden: true
        }
    });

    columns.push({
        name: 'Category.Name',
       
      index: 'Category',
      width: 150
    });
     columns.push({
        name: 'UrlSlug',
        index: 'UrlSlug',
      width: 200,
      sortable: false,
      editable: true,
      editoptions: {
        size: 43,
        maxlength: 200
      },
      editrules: {
        required: true
      }
    });

    columns.push({
        name: 'CreatedOn',
        index: 'CreatedOn',
      width: 150,
      align: 'center',
      sorttype: 'date',
      datefmt: 'm/d/Y'
    });

    columns.push({
        name: 'Published',
        index: 'Published',
        width: 100,
        align: 'center',
        editable: true,
        edittype: 'checkbox',
        editoptions: {
            value: "true:false",
            defaultValue: 'false'
        }
    });
      columns.push({
          name: 'Album.Id',
          index: 'AlbumId',
          hidden: true,
          editable: true,
          edittype: 'select',
          editoptions: {
              style: 'width:250px;',
              dataUrl: '/Admin/GetPhotoAlbumsHtml'
          },
          editrules: {
              required: true,
              edithidden: true
          }
      });

      columns.push({
          name: 'Album.Name',

          index: 'Album',
          width: 150
      });

    $(gridName).jqGrid({
      url: '/Admin/Posts',
      datatype: 'json',
      mtype: 'GET',
      height: 'auto',
      toppager: true,

      colNames: colNames,
      colModel: columns,

      pager: pagerName,
      rownumbers: true,
      rownumWidth: 40,
      rowNum: 10,
      rowList: [10, 20, 30],

      sortname: 'CreatedOn',
      sortorder: 'desc',
      viewrecords: true,

      jsonReader: {
        repeatitems: false
      },

      afterInsertRow: function (rowid, rowdata, rowelem) {
        var published = rowdata["Published"];

        if (!published) {
          $(gridName).setRowData(rowid, [], {
            color: '#9D9687'
          });
          $(gridName + " tr#" + rowid + " a").css({
            color: '#9D9687'
          });
        }

       
      }
    });

    // configuring add options
    var addOptions = {
      url: '/Admin/AddPost',
      addCaption: 'Add Post',
      processData: "Saving...",
      width: 900,
      closeAfterAdd: true,
      closeOnEscape: true,
      afterclickPgButtons: afterclickPgButtons,
      afterShowForm: afterShowForm,
      onClose: onClose,
      afterSubmit: JustBlog.GridManager.afterSubmitHandler,
      beforeSubmit: beforeSubmitHandler
    };

    var editOptions = {
      url: '/Admin/EditPost',
      editCaption: 'Edit Post',
      processData: "Saving...",
      width: 900,
      closeAfterEdit: true,
      closeOnEscape: true,
      afterclickPgButtons: afterclickPgButtons,
      afterShowForm: afterShowForm,
      onClose: onClose,
      afterSubmit: JustBlog.GridManager.afterSubmitHandler,
      beforeSubmit: beforeSubmitHandler
    };

    var deleteOptions = {
      url: '/Admin/DeletePost',
      caption: 'Delete Post',
      processData: "Saving...",
      msg: "Delete the Post?",
      closeOnEscape: true,
      afterSubmit: JustBlog.GridManager.afterSubmitHandler
    };

    $(gridName).navGrid(pagerName, { cloneToTop: true, search: false }, editOptions, addOptions, deleteOptions);
  };

 // ************************* CATEGORIES GRID
  JustBlog.GridManager.categoriesGrid = function (gridName, pagerName) {
      //*** Event handlers
      var afterclickPgButtons = function (whichbutton, formid, rowid) {
          tinyMCE.get("Description").setContent(formid[0]["Description"].value);
         
      };

      var afterShowForm = function (form) {
          tinyMCE.execCommand('mceAddControl', false, "Description");
      };

      var onClose = function (form) {
          tinyMCE.execCommand('mceRemoveControl', false, "Description");
      };

      var beforeSubmitHandler = function (postdata, form) {
          postdata.Description = tinyMCE.get("Description").getContent();
          return [true];
      };
      var colNames = ['id', 'Name', 'UrlSlug', 'NameEN', "Meta",'Description', 'AlbumId',
        'Album'];

    var columns = [];

    columns.push({
        name: 'id',
        index: 'id',
        width: 200,
        hidden: true,
        sortable: false,
        editable: true,
        editoptions: {
            size: 43,
            maxlength: 200
        },
        editrules: {
            required: false
        }
    });
    columns.push({
        name: 'UrlSlug',
        index: 'UrlSlug',
        width: 200,
        editable: true,
        edittype: 'text',
        sortable: false,
        editoptions: {
            size: 30,
            maxlength: 50
        },
        editrules: {
            required: true
        }
    });
    columns.push({
      name: 'Name',
      index: 'Name',
      width: 200,
      editable: true,
      edittype: 'text',
      editoptions: {
        size: 30,
        maxlength: 50
      },
      editrules: {
        required: true
      }
    });
      columns.push({
          name: 'Meta',
          index: 'Meta',
          width: 250,
          sortable: false,
          editable: true,
          edittype: 'textarea',
          editoptions: {
              rows: "2",
              cols: "40",
              maxlength: 1000
          },
          editrules: {
              required: true
          }
      });
    columns.push({
        name: 'NameEN',
        index: 'NameEN',
        width: 200,
        editable: true,
        edittype: 'text',
        editoptions: {
            size: 30,
            maxlength: 50
        },
        editrules: {
            required: true
        }
    });
    columns.push({
      name: 'Description',
      index: 'Description',
      width: 200,
      editable: true,
      sortable: false,
      hidden: true,
      edittype: 'textarea',
      editoptions: {
          rows: "7",
          cols: "10"
      },
      editrules: {
          custom: true,
          custom_func: function (val, colname) {
              val = tinyMCE.get("Description").getContent();
              if (val) return [true, ""];
              return [false, colname + ": Field is requred"];
          },
          edithidden: true
      }
    });
    columns.push({
        name: 'Album.Id',
        index: 'AlbumId',
        hidden: true,
        editable: true,
        edittype: 'select',
        editoptions: {
            style: 'width:250px;',
            dataUrl: '/Admin/GetPhotoAlbumsHtml'
        },
        editrules: {
            required: true,
            edithidden: true
        }
    });

    columns.push({
        name: 'Album.Name',

        index: 'Album',
        width: 150
    });
    $(gridName).jqGrid({
      url: '/Admin/Categories',
      datatype: 'json',
      mtype: 'GET',
      height: 'auto',
      toppager: true,
      colNames: colNames,
      colModel: columns,
      pager: pagerName,
      rownumbers: true,
      rownumWidth: 40,
      rowNum: 500,
      sortname: 'Name',
      loadonce: true,
      jsonReader: {
        repeatitems: false
      }
    });

    var editOptions = {
      url: '/Admin/EditCategory',
      width: 600,
      editCaption: 'Edit Category',
      processData: "Saving...",
      closeAfterEdit: true,
      closeOnEscape: true,
      afterSubmit: JustBlog.GridManager.afterSubmitHandler,
      afterclickPgButtons: afterclickPgButtons,
      afterShowForm: afterShowForm,
      onClose: onClose,
      beforeSubmit: beforeSubmitHandler
    };

    var addOptions = {
      url: '/Admin/AddCategory',
      width: 600,
      addCaption: 'Add Category',
      processData: "Saving...",
      type:"POST",
      closeAfterAdd: true,
      closeOnEscape: true,
      afterSubmit:JustBlog.GridManager.afterSubmitHandler,
       afterclickPgButtons: afterclickPgButtons,
      afterShowForm: afterShowForm,
      onClose: onClose,
      beforeSubmit: beforeSubmitHandler
    };

    var deleteOptions = {
      url: '/Admin/DeleteCategory',
      caption: 'Delete Category',
      processData: "Saving...",
      width: 500,
      msg: "Delete the category? This will delete all the posts belonged to this category as well.",
      closeOnEscape: true,
      afterSubmit: JustBlog.GridManager.afterSubmitHandler
    };

    // configuring the navigation toolbar.
    $(gridName).jqGrid('navGrid', pagerName, {
      cloneToTop: true,
      search: false
    },

    editOptions, addOptions, deleteOptions);
  };

 // ************************* Services GRID
  JustBlog.GridManager.tagsGrid = function (gridName, pagerName) {
      //*** Event handlers
      var afterclickPgButtons = function (whichbutton, formid, rowid) {
          tinyMCE.get("Description").setContent(formid[0]["Description"].value);

      };

      var afterShowForm = function (form) {
          tinyMCE.execCommand('mceAddControl', false, "Description");
      };

      var onClose = function (form) {
          tinyMCE.execCommand('mceRemoveControl', false, "Description");
      };

      var beforeSubmitHandler = function (postdata, form) {
          postdata.Description = tinyMCE.get("Description").getContent();
          return [true];
      };
      var colNames = ['id', 'Name', "Meta",'UrlSlug', 'NameEN', 'Description'];

      var columns = [];

      columns.push({
          name: 'id',
          index: 'id',
          width: 200,
          hidden: true,
          sortable: false,
          editable: true,
          editoptions: {
              size: 43,
              maxlength: 200
          },
          editrules: {
              required: false
          }
      });
      columns.push({
          name: 'UrlSlug',
          index: 'UrlSlug',
          width: 200,
          editable: true,
          edittype: 'text',
          sortable: false,
          editoptions: {
              size: 30,
              maxlength: 50
          },
          editrules: {
              required: true
          }
      });
      columns.push({
          name: 'Name',
          index: 'Name',
          width: 200,
          editable: true,
          edittype: 'text',
          editoptions: {
              size: 30,
              maxlength: 50
          },
          editrules: {
              required: true
          }
      });
      columns.push({
          name: 'Meta',
          index: 'Meta',
          width: 250,
          sortable: false,
          editable: true,
          edittype: 'textarea',
          editoptions: {
              rows: "2",
              cols: "40",
              maxlength: 1000
          },
          editrules: {
              required: true
          }
      });
      columns.push({
          name: 'NameEN',
          index: 'NameEN',
          width: 200,
          editable: true,
          edittype: 'text',
          editoptions: {
              size: 30,
              maxlength: 50
          },
          editrules: {
              required: true
          }
      });
      columns.push({
          name: 'Description',
          index: 'Description',
          width: 200,
          editable: true,
          sortable: false,
          hidden: true,
          edittype: 'textarea',
          editoptions: {
              rows: "7",
              cols: "10"
          },
          editrules: {
              custom: true,
              custom_func: function (val, colname) {
                  val = tinyMCE.get("Description").getContent();
                  if (val) return [true, ""];
                  return [false, colname + ": Field is requred"];
              },
              edithidden: true
          }
      });

      $(gridName).jqGrid({
          url: '/Admin/Services',
          datatype: 'json',
          mtype: 'GET',
          height: 'auto',
          toppager: true,
          colNames: colNames,
          colModel: columns,
          pager: pagerName,
          rownumbers: true,
          rownumWidth: 40,
          rowNum: 500,
          sortname: 'Name',
          loadonce: true,
          jsonReader: {
              repeatitems: false
          }
      });

      var editOptions = {
          url: '/Admin/EditServices',
          width: 600,
          editCaption: 'Edit Service',
          processData: "Saving...",
          closeAfterEdit: true,
          closeOnEscape: true,
          afterSubmit: JustBlog.GridManager.afterSubmitHandler,
          afterclickPgButtons: afterclickPgButtons,
          afterShowForm: afterShowForm,
          onClose: onClose,
          beforeSubmit: beforeSubmitHandler
      };

      var addOptions = {
          url: '/Admin/AddService',
          width: 600,
          addCaption: 'Add Service',
          processData: "Saving...",
          type: "POST",
          closeAfterAdd: true,
          closeOnEscape: true,
          afterSubmit: JustBlog.GridManager.afterSubmitHandler,
          afterclickPgButtons: afterclickPgButtons,
          afterShowForm: afterShowForm,
          onClose: onClose,
          beforeSubmit: beforeSubmitHandler
      };

      var deleteOptions = {
          url: '/Admin/DeleteServices',
          caption: 'Delete Service',
          processData: "Saving...",
          width: 500,
          msg: "Delete the service?",
          closeOnEscape: true,
          afterSubmit: JustBlog.GridManager.afterSubmitHandler
      };

      // configuring the navigation toolbar.
      $(gridName).jqGrid('navGrid', pagerName, {
          cloneToTop: true,
          search: false
      },

          editOptions, addOptions, deleteOptions);
  };

  JustBlog.GridManager.afterSubmitHandler = function (response, postdata) {

    var json = $.parseJSON(response.responseText);

    if (json) return [json.success, json.message, json.id];

    return [false, "Failed to get result from server.", null];
  };
  $("#IdSubmit").click(function () {
      $.ajax({
          url: '/Feedback/AddFeedback',
          dataType: "json",
          type: "POST",
          contentType: 'application/json; charset=utf-8',
          data: JSON.stringify({
              feedback: { name: $("#nameid").val(), linkprofile: $("#socialid").val(), body: $("#feedbackBody").val() }
          }),
          async: true,
          processData: false,
          cache: false,
          alert: alert('Отзыв добавлен')
          //success: function(data) {
          //    alert(data);
          //},
          //error: function(xhr) {
          //    alert('error');
          //}
      });

  });
  $("#tabs").tabs({
    show: function (event, ui) {

      if (!ui.tab.isLoaded) {

        var gdMgr = JustBlog.GridManager,
    			fn, gridName, pagerName;

        switch (ui.index) {
          case 0:
            fn = gdMgr.postsGrid;
            gridName = "#tablePosts";
            pagerName = "#pagerPosts";
            break;
          case 1:
            fn = gdMgr.categoriesGrid;
            gridName = "#tableCats";
            pagerName = "#pagerCats";
            break;
          case 2:
            fn = gdMgr.tagsGrid;
            gridName = "#tableServices";
            pagerName = "#pagerServices";
            break;
        };

        fn(gridName, pagerName);
        ui.tab.isLoaded = true;
      }
    }
  });
  
});




























