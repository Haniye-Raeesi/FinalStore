const cookieName = "cart-items";

function addToCart(id, name, price, picture, Discount) {

    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();
    const currentProduct = products.find(x => x.id === id);
    if (currentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            Discount,
            count
        }

        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    UpdateCard();
}

function UpdateCard() {

    let Products = $.cookie(cookieName);
    Products = JSON.parse(Products);
    $("#card_items_Num").text(Products.length);

    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    Products.forEach(x => {
        const product = `<div class="single-cart-item">
                            <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${x.id}')">
                                <i class="ion-android-close"></i>
                            </a>
                            <div class="image">
                                <a href="single-product.html">
                                    <img src="/ProductPictures/${x.picture}"
                                         class="img-fluid" alt="">
                                </a>
                            </div>
                            <div class="content">
                                <p class="product-title">
                                    <a href="single-product.html">محصول: ${x.name}</a>
                                </p>
                                <p class="count">تعداد: ${x.count}</p>
                                <p class="count">قیمت واحد: ${x.unitPrice}</p>
                            </div>
                        </div>`;
        cartItemsWrapper.append(product);
    });       
}
function removeFromCart(id) {
    let Products = $.cookie(cookieName);
    Products = JSON.parse(Products);
    const ItemToRemove = Products.findIndex(x => x.id == id);
    Products.splice(ItemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(Products), { expires: 2, path: "/" });
    UpdateCard();
}
function ChangeCartItemCount(id, CustomCount,TotPrice) {
    //debugger;
    let Products = $.cookie(cookieName);
    Products = JSON.parse(Products);
    const ProductIndex = Products.findIndex(x => x.id == id);
    var productToCange = Products[ProductIndex];
    productToCange.count = CustomCount;
    var TotalPrice = productToCange.unitPrice * CustomCount;
    //TotPrice.text = TotalPrice;
    $(`#${TotPrice}`).text(TotalPrice);
    $.cookie(cookieName, JSON.stringify(Products), { expires: 2, path: "/" });
    location.reload();
    UpdateCard();

}
       

