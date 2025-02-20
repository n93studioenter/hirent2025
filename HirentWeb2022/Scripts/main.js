(function($) {
  "use strict"

  // NAVIGATION
  var responsiveNav = $('#responsive-nav'),
    catToggle = $('#responsive-nav .category-nav .category-header'),
    catList = $('#responsive-nav .category-nav .category-list'),
    menuToggle = $('#responsive-nav .menu-nav .menu-header'),
    menuList = $('#responsive-nav .menu-nav .menu-list');

  catToggle.on('click', function() {
    menuList.removeClass('open');
    catList.toggleClass('open');
  });

  menuToggle.on('click', function() {
    catList.removeClass('open');
    menuList.toggleClass('open');
  });

  $(document).click(function(event) {
    if (!$(event.target).closest(responsiveNav).length) {
      if (responsiveNav.hasClass('open')) {
        responsiveNav.removeClass('open');
        $('#navigation').removeClass('shadow');
      } else {
        if ($(event.target).closest('.nav-toggle > button').length) {
          if (!menuList.hasClass('open') && !catList.hasClass('open')) {
            menuList.addClass('open');
          }
          $('#navigation').addClass('shadow');
          responsiveNav.addClass('open');
        }
      }
    }
  });
    $('#product-chonthem').slick({
        slidesToShow: 2,
        slidesToScroll: 2,
        autoplay: true,
        infinite: true,
        speed: 300,
        dots: false,
        arrows: true,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
            }   
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 2,
                slidesToScroll: 2,
            }
        },
        ]
    });
  // HOME SLICK
  $('#home-slick').slick({
    autoplay: true,
    infinite: true,
    speed: 300,
      arrows: false,
      dots: true,
  });
    $('#productcate-slick-1').slick({
        slidesToShow:6,
        slidesToScroll: 1,
        autoplay: false,
        infinite: true,
        speed: 300,
        dots: false,
        arrows: true,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        },
        ]
    });
    $('#productcate-slick-2').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: false,
        infinite: true,
        speed: 300,
        dots: false,
        arrows: true,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        },
        ]
    });
    $('#productcate-slick-3').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: false,
        infinite: true,
        speed: 300,
        dots: false,
        arrows: true,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        },
        ]
    });
    $('#productcate-slick-4').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: false,
        infinite: true,
        speed: 300,
        dots: false,
        arrows: true,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        },
        ]
    });
    $('#productcate-slick-5').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: false,
        infinite: true,
        speed: 300,
        dots: false,
        arrows: true,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        },
        ]
    });
    $('#productcate-slick-6').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: false,
        infinite: true,
        speed: 300,
        dots: false,
        arrows: true,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        },
        ]
    });
    $('#productcate-slick-7').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: false,
        infinite: true,
        speed: 300,
        dots: false,
        arrows: true,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 2,
                slidesToScroll: 1,
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1,
            }
        },
        ]
    });
  $('#product-slick-1').slick({
    slidesToShow: 5,
    slidesToScroll:6,
    autoplay: true,
    infinite: true,
      speed: 2000,
      autoplaySpeed: 7000,
    dots: false,
    arrows: true,
    appendDots: '.product-slick-dots-1',
    responsive: [{
        breakpoint: 991,
        settings: {
            slidesToShow: 5,
            slidesToScroll:5,
        }
    },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: false,
                slidesToShow: 2,
                slidesToScroll:2,
            }
        },
      {
        breakpoint: 480,
        settings: {
          dots: false,
            arrows: false,
          slidesToShow:2,
          slidesToScroll: 2,
        }
      },
    ]
  });

  $('#product-slick-2').slick({
      slidesToShow: 5,
      slidesToScroll:5,
      autoplay: true,
      infinite: true,
      autoplaySpeed: 7000,
      speed: 2000,
      dots: false,
      arrows: true,
    appendDots: '.product-slick-dots-2',
    responsive: [{
        breakpoint: 991,
        settings: {
            slidesToShow: 5,
            slidesToScroll:5,
        }
    },
        {
            breakpoint: 640,
            settings: {
                dots: false,
                arrows: false,
                slidesToShow: 2,
                slidesToScroll: 2,
            }
        },
      {
        breakpoint: 480,
        settings: {
          dots: false,
            arrows: false,
          slidesToShow: 2,
          slidesToScroll:2,
        }
      },
    ]
  });

  $('#product-slick-3').slick({
      slidesToShow: 5,
      slidesToScroll: 5,
      autoplay: true,
      infinite: true,
      speed: 2000,
      autoplaySpeed: 7000,
      dots: false,
      arrows: true,
      appendDots: '.product-slick-dots-2',
      responsive: [{
          breakpoint: 991,
          settings: {
              slidesToShow: 5,
              slidesToScroll: 6,
          }
      },
          {
              breakpoint: 640,
              settings: {
                  dots: false,
                  arrows: false,
                  slidesToShow: 2,
                  slidesToScroll: 2,
              }
          },
      {
          breakpoint: 480,
          settings: {
              dots: false,
              arrows: false,
              slidesToShow: 2,
              slidesToScroll: 2,
          }
      },
      ]
  });

  $('#product-slick-4').slick({
      slidesToShow: 5,
      slidesToScroll:5,
      autoplay: true,
      infinite: true,
      speed: 2000,
      autoplaySpeed: 7000,
      dots: false,
      arrows: true,
      appendDots: '.product-slick-dots-2',
      responsive: [{
          breakpoint: 991,
          settings: {
              slidesToShow: 5,
              slidesToScroll: 6,
          }
      },
          {
              breakpoint: 640,
              settings: {
                  dots: false,
                  arrows: false,
                  slidesToShow: 2,
                  slidesToScroll: 2,
              }
          },
      {
          breakpoint: 480,
          settings: {
              dots: false,
              arrows: false,
              slidesToShow:2,
              slidesToScroll: 2,
          }
      },
      ]
  });

  $('#product-slick-5').slick({
      slidesToShow: 5,
      slidesToScroll:5,
      autoplay: true,
      infinite: true,
      speed: 2000,
      autoplaySpeed:7000,
      dots: false,
      arrows: true,
      appendDots: '.product-slick-dots-2',
      responsive: [{
          breakpoint: 991,
          settings: {
              slidesToShow: 5,
              slidesToScroll: 6,
          }
      },
          {
              breakpoint: 640,
              settings: {
                  dots: false,
                  arrows: false,
                  slidesToShow: 2,
                  slidesToScroll: 2,
              }
          },
      {
          breakpoint: 480,
          settings: {
              dots: false,
              arrows: false,
              slidesToShow:2,
              slidesToScroll: 2,
          }
      },
      ]
  });

  $('#product-slick-6').slick({
      slidesToShow: 5,
      slidesToScroll:5,
      infinite: true,
      speed: 300,
      dots: false,
      arrows: true,
      appendDots: '.product-slick-dots-2',
      responsive: [{
          breakpoint: 991,
          settings: {
              slidesToShow: 5,
              slidesToScroll: 6,
          }
      },
          {
              breakpoint: 640,
              settings: {
                  dots: false,
                  arrows: false,
                  slidesToShow: 2,
                  slidesToScroll: 2,
              }
          },
      {
          breakpoint: 480,
          settings: {
              dots: false,
              arrows: false,
              slidesToShow:2,
              slidesToScroll: 2,
          }
      },
      ]
  });

  $('#product-slick-7').slick({
      slidesToShow: 5,
      slidesToScroll: 6,
      autoplay: false,
      infinite: true,
      speed: 300,
      dots: false,
      arrows: true,
      appendDots: '.product-slick-dots-2',
      responsive: [{
          breakpoint: 991,
          settings: {
              slidesToShow: 5,
              slidesToScroll: 5,
          }
      },
          {
              breakpoint: 640,
              settings: {
                  dots: false,
                  arrows: false,
                  slidesToShow: 2,
                  slidesToScroll: 2,
              }
          },
      {
          breakpoint: 480,
          settings: {
              dots: false,
              arrows: false,
              slidesToShow: 2,
              slidesToScroll:2,
          }
      },
      ]
  });


  // PRODUCT DETAILS SLICK
    $('product-main-view1').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        centerMode: true,
        focusOnSelect: true,
    });
    $('#product-main-view').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
    infinite: true,
    speed: 300,
    dots: false,
    arrows: true,
    fade: true,
    asNavFor: '#product-view',
  });

  $('#product-view').slick({
    slidesToShow: 3,
    slidesToScroll: 1,
    arrows: true,
    centerMode: true,
    focusOnSelect: true,
    asNavFor: '#product-main-view',
  });
    $('#product-view2').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        arrows: true,
        centerMode: true,
        focusOnSelect: true,
        asNavFor: '#product-main-view',
    });
  

  // PRODUCT ZOOM
  //$('#product-main-view .product-view').zoom();
    var numleft = "";
    var numright = "";
  // PRICE SLIDER
  var slider = document.getElementById('price-slider');
  if (slider) {
    noUiSlider.create(slider, {
        start: [1, 500000],
      connect: true,
      tooltips: [true, true],
      format: {
        to: function(value) {
          return value.toFixed(0) + ' đ';
        },
        from: function(value) {
          return value
        }
      },
      range: {
        'min': 1000,
        'max': 500000
      }
    });

      var nodes = [
        document.getElementById('lower-value'), // 0
        document.getElementById('upper-value')  // 1
    ];
      slider.noUiSlider.on('end', function (values, handle, unencoded, isTap, positions) {
          numleft = values[0];
          numright = values[1];
          var xmlhttp;
          if (window.XMLHttpRequest) {
              xmlhttp = new XMLHttpRequest();
          }
          else {
              xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
          }
          xmlhttp.onreadystatechange = function () {

              if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                  $('#MainContent_dvLoadProductList').html(xmlhttp.responseText);
              }
          }
          var aa = location.search.replace('?', '');
          var cchon = $("#radChon").val();
          xmlhttp.open("GET", "../Ajax.aspx?action=searchproductbyprice&bp=" + numleft + "&be=" + numright + "&cchon=" + cchon, true);
          xmlhttp.send();
      });
    

    }


    //Theo ngày
    //var slider2 = document.getElementById('price-slider2');
    //if (slider2) {
    //    noUiSlider.create(slider2, {
    //        start: [1, 2000000],
    //        connect: true,
    //        tooltips: [true, true],
    //        format: {
    //            to: function (value) {
    //                return value.toFixed(0) + ' đ';
    //            },
    //            from: function (value) {
    //                return value
    //            }
    //        },
    //        range: {
    //            'min': 1000,
    //            'max': 2000000
    //        }
    //    });

    //    var nodes = [
    //        document.getElementById('lower-value'), // 0
    //        document.getElementById('upper-value')  // 1
    //    ];
    //    slider.noUiSlider.on('update', function (values, handle, unencoded, isTap, positions) {
    //        numleft = values[0];
    //        numright = values[1];
    //        var xmlhttp;
    //        if (window.XMLHttpRequest) {
    //            xmlhttp = new XMLHttpRequest();
    //        }
    //        else {
    //            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    //        }
    //        xmlhttp.onreadystatechange = function () {

    //            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
    //                $('#MainContent_dvLoadProductList').html(xmlhttp.responseText);
    //            }
    //        }
    //        var aa = location.search.replace('?', '');
    //        xmlhttp.open("GET", "../Ajax.aspx?action=searchproductbyprice&bp=" + numleft + "&be=" + numright + "&" + aa, true);
    //        xmlhttp.send();
    //    });


    //}
    //var nonLinearSlider = document.getElementById('price-slider');

    //noUiSlider.create(nonLinearSlider, {
    //    connect: true,
    //    behaviour: 'tap',
    //    start: [500, 4000],
    //    range: {
    //        // Starting at 500, step the value by 500,
    //        // until 4000 is reached. From there, step by 1000.
    //        'min': [0],
    //        '10%': [500, 500],
    //        '50%': [4000, 1000],
    //        'max': [10000]
    //    }
    //});
    //var nodes = [
    //    document.getElementById('lower-value'), // 0
    //    document.getElementById('upper-value')  // 1
    //];

    //// Display the slider value and how far the handle moved
    //// from the left edge of the slider.
    //nonLinearSlider.noUiSlider.on('update', function (values, handle, unencoded, isTap, positions) {
    //    alert('asdsd')
    //    nodes[handle].innerHTML = values[handle] + ', ' + positions[handle].toFixed(2) + '%';
    //});
})(jQuery);
