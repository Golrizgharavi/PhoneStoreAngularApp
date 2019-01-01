'use strict'
/*
app.controller('homeController', function($scope) {

    
    //$scope.message = "Welcome";
})
*/
function AutoPlaySlider() {

    var mySlides = document.getElementsByClassName("myHeaderSlides");
    var myDots = document.getElementsByClassName("SliderDots");
    var slideIndex = 1;
    var timer = null;
    var myIndex = 0;

    var _this = this;


    this.SlidesOff = function (side) {

        var i;
        for (i = 0; i < mySlides.length; i++) {
            $(mySlides[i]).animate({ opacity: '0' });
            mySlides[i].style.display = "none";

            //if (side === 'L')
            //    mySlides[i].style.left = "100%";
            //if (side === 'R')
            //    mySlides[i].style.left = "-100%";
        }

        for (i = 0; i < myDots.length; i++) {
            myDots[i].className = myDots[i].className.replace(" activeDot", "");
        }

    };

    this.MoveSlide = function (n, side) {
        clearTimeout(timer);
        this.ShowSlide(slideIndex += n, side);
    };

    this.CurrentDot = function (n, side) {
        clearTimeout(timer);
        this.ShowSlide(slideIndex = n, side);
    };

    this.stopShow = function (timer) {

        clearInterval(timer);
    };

    this.startShow = function () {
        timer = setTimeout(function () { _this.ShowSlide() }, 5000);
    };

    this.SlideHover = function () {
        $('#HeaderSliderHolder').hover(function () {

            _this.stopShow(timer);
        }, function () {

            timer = setTimeout(function () { _this.ShowSlide() }, 5000);
        });

    };
    this.ShowSlide = function (n, side) {
        if (n == undefined) { n = ++slideIndex };
        if (n > mySlides.length) { slideIndex = 1 };
        if (n < 1) { slideIndex = mySlides.length };

        this.SlidesOff(side);
        mySlides[slideIndex - 1].style.display = "block";
        myDots[slideIndex - 1].className += " activeDot";
        $(mySlides[slideIndex - 1]).animate({ opacity: '1' });
        // $(mySlides[slideIndex - 1]).animate({ left: '0px' });

        timer = setTimeout(function () { _this.ShowSlide() }, 5000);


    };

    this.ShowSlide(slideIndex);
    this.SlideHover();

}

app.controller('homeController', function (productService) {

    var vm = this;
    vm.message = "Golriz Test";
    var $Carouselnner = document.getElementById("Carousel-inner");
   // var $sliderItemsHolder = document.getElementById("sliderItemsHolder");
    //var promiseGet = productService.getProductCount();
    var promiseGet = productService.GetHomePage(null);


    vm.SetCarousel = function () {

        $('#myCarousel').carousel({
            interval: 6000
        });

        $('.carousel .item').each(function () {
            //  alert('**********');
            var next = $(this).next();
            if (!next.length) {
                next = $(this).siblings(':first');
            }
            next.children(':first-child').clone().appendTo($(this));

            for (var i = 0; i < 2; i++) {
                next = next.next();
                if (!next.length) {
                    next = $(this).siblings(':first');
                }

                next.children(':first-child').clone().appendTo($(this));
            }

            $('.carousel-control.right').click(function () { $('.carousel').carousel('next') });
            $('.carousel-control.left').click(function () { $('.carousel').carousel('prev') });
        });

    },

    vm.SetTopSlider = function (ObjArr) {
            var Temp = "";
            for (var i = 0; i < ObjArr.length; i++) {
                Temp += '<div class="myHeaderSlides" ng-repeat="slide in homeControllerVM.TopSlider">' +
                    '<span class="HeaderSlidesTittle">' + ObjArr[i].t + '  ' + i + '</span>' +
                    '<span class="HeaderSlidesContent">' + ObjArr[i].d + '</span>' +
                    '</div>';
            }
            Temp += '<div class="DotsHolder">';
            for (var i = 0; i < ObjArr.length; i++) {

                if (i == 0)
                    Temp += '<span class="SliderDots activeDot" onclick="myAutoPlaySlide.CurrentDot(' + (i + 1) + ',\'R\')"></span>';
                else
                    Temp += '<span class="SliderDots " onclick="myAutoPlaySlide.CurrentDot(' + (i + 1) + ',\'R\')"></span>';
            }
            Temp += '</div>' +
                '<div style="width:100%">' +
                '<div class="Arrowleft" onclick="myAutoPlaySlide.MoveSlide(-1,\'L\')">&#10094;</div>' +
                '<div class="ArrowRight " onclick="myAutoPlaySlide.MoveSlide(1,\'R\')">&#10095;</div>' +
                '</div>'

        $sliderItemsHolder.innerHTML = Temp;

     
        },

    promiseGet.then(function (p1) {
        vm.devices = p1.data;
        vm.DevCount = vm.devices.Devs; 
        vm.TopSlider = vm.devices.topSl;
        var myObjs = vm.devices.DataArr;
        var $myTemplate = "";

       

        for (var i = 0; i < myObjs.length; i++) {
            if (i == 0)
                $myTemplate += '<div class="item active">';
            else
                $myTemplate += '<div class="item">';
            

            $myTemplate += '<div class="item-item col-md-3 col-sm-4">';
            $myTemplate += (myObjs[i].Sale == 'True') ? '<span class="CircleOffer">SPECIAL OFFER</span>' : '';
            $myTemplate += '<a href = "#/' + myObjs[i].PrType + '/' + myObjs[i].Id + '" target="_blank">' +
                '<img src="' + myObjs[i].ImgUrl + '" class="img-responsive">' +
                '</a>' +
                '<b class="CarouselItemTittle">' + myObjs[i].Brand + '&nbsp;' + myObjs[i].Name + '</b>' +
                '<span class="CarouselItemDescription">' + myObjs[i].Summery + '</span>' +
                '<a href="#/' + myObjs[i].PrType + '/' + myObjs[i].Id + '" class="CarouselItemLink" target="_blank">Check it out! &raquo;</a>' +
                '</div >' +
                '</div>';
        }
            
       $Carouselnner.innerHTML = $myTemplate;
       // alert('back');
        vm.SetCarousel();
     
       
       
    },
        function (errorP1) {
            alert('FAILURE LOADING Products', errorP1)
        });



});


/*---------------Carousel Slider-----------------*/

