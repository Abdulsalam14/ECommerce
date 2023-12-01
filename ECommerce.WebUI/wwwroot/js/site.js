
$(document).ready(function () {
    $("#Inputt").on("input", function () {
        let key = $(this).val();

        if (key.trim() !== "") {
            $.ajax({
                url: "/Product/Search",
                method: "GET",
                data: { key: key },
                success: function (data) {
                    console.log( data);

                    let content = `
                    
                    <table class="table">
                        <thead>
                             <tr>
                                <th>ProductName</th>
                                <th>UnitPrice</th>
                                <th>UnitsInStock</th>
                                <th></th>
                             </tr>
                        </thead>
                        <tbody>`;
                    for (var i = 0; i < data.length; i++) {
                        let item = data[i];
                        console.log(item)
                        content += `
                                <tr>
                                    <td>
                                        ${item.productName}
                                    </td>
                                    <td>
                                        ${item.unitPrice}
                                    </td>
                                    <td>
                                        ${item.unitsInStock}
                                    </td>
                                    <td>
                                        <a class="btn btn-xs btn-success" href="/Cart/AddToCart?productId=${item.productId}&page=1">Add To Cart</a>
                                    </td>
                        `
                        if (item.hasAdded) {
                            content += `
                            <td>
                                <a class="btn btn-xs btn-danger" href="/Cart/Remove?productId=${item.productId}&page=1" &filteraz="@filter1" &filterhl="@filter2">Remove</a>
                            </td >
                        </tr>
                        `
                        }
                        else content += `</tr>`
                    }

                    content += `
                        </tbody>
                    </table>
                    `
                    $(".col-md-10").html(content);
                    console.log(content)
                },

                error: function (error) {
                    console.error(error);
                }
            });
        }
    });
});