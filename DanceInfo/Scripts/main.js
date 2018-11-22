'use strict'

window.onload = () => {

    //Comments

    $(".news").bind('click', (event) => {
        location.replace(event.currentTarget.dataset.href)
    });

    $('#submitComment').bind('click', (event) => {
        $.ajax({
            method: "POST",
            url: window.location.href,
            data: { scController: "Comments", scAction: "AddComment", comment: addComment.value, id: event.currentTarget.dataset.itemid },
        })
            .done(function (msg) {
                let newComment = document.createElement('li');
                newComment.className = 'media';
                newComment.innerHTML = `
                            <a class="pull-left" href="#">
                                <img class="media-object img-circle" src="https://s3.amazonaws.com/uifaces/faces/twitter/dancounsell/128.jpg" alt="profile">
                            </a>
                            <div class="media-body">
                                <div class="well well-lg">
                                    <h4 class="media-heading text-uppercase reviews">Author</h4>
                                    <ul class="media-date text-uppercase reviews list-inline">
                                        <li class="dd">22</li>
                                        <li class="mm">09</li>
                                        <li class="aaaa">2014</li>
                                    </ul>
                                    <p class="media-comment">
                                        ${addComment.value}
                                    </p>
                                </div>
                            </div>
                `;
                listComments.appendChild(newComment);
                addComment.value = '';
            });

    });
    //new custom functions

    Object.ToArrayFilter = (obj, predicate) =>
        [...Object.keys(obj)
            .filter(key => predicate(obj[key]))
            .map(key => key)];   

    ////// class PostsSearch
    class PostsSearch {
        constructor() {
            this.InfoSearchPosts = {
                page: 1,
                query: '',
                categories: {},
                tags: {}
            };
            this.Posts;
            this.QuantityPage;
        }

        createSearchPosts() {
            $('#search').bind('keyup', () => {
                this.newValueSearchQuery();
            });

            $("input[type=checkbox]").on("click", (event) => {
                search.focus();
                this.newValueSearchCheckBox(event);
            });
            $("#search").on("focus", () => {
                $("#searchInfo").addClass('open');
            });
            $("#search").on("blur", () => {
                $("#searchInfo").removeClass('open');
            });
            postsSearch.OnClickPagination();
        }

        nextPage() {
            if (this.InfoSearchPosts.page + 1 <= this.QuantityPage) {
                this.InfoSearchPosts.page++;
                this.getNewPosts();
            }
        }

        prevPage() {
            if (this.InfoSearchPosts.page - 1 > 0) {
                this.InfoSearchPosts.page--;
                this.getNewPosts();
            }
        }

        setNewPage(event) {
            this.InfoSearchPosts.page = event.currentTarget.dataset.page;
            this.getNewPosts();
        }

        newValueSearchCheckBox(event) {
            if (event.currentTarget.name === 'categories')
                this.InfoSearchPosts.categories[event.currentTarget.dataset.category] = event.currentTarget.checked
            else if (event.currentTarget.name === 'tags')
                this.InfoSearchPosts.tags[event.currentTarget.dataset.tag] = event.currentTarget.checked;
            this.getNewPosts();
        }

        newValueSearchQuery() {
            this.InfoSearchPosts.query = search.value;
            this.getNewPosts();
        }

        OnClickPagination() {
            $("#prev").on("click", () => {
                this.prevPage();
            });
            $("#next").on("click", () => {
                this.nextPage();
            });
            $(".numberPage").on("click", (event) => {
                this.setNewPage(event);
            });
            this.QuantityPage = $(".numberPage").length;
        }

        getNewPosts() {
            let dataInfoSearchPosts = {
                scController: "PostsSearch",
                scAction: "GetNewPosts"
            };
            for (let key in this.InfoSearchPosts) {
                if (this.InfoSearchPosts[key] !== null &&
                    (typeof this.InfoSearchPosts[key] === 'object' ?
                        !$.isEmptyObject(this.InfoSearchPosts[key]) : true)
                ) {
                    if (typeof this.InfoSearchPosts[key] === 'object') {
                        let arr = Object.ToArrayFilter(this.InfoSearchPosts[key], elem => elem === true);
                        arr.length === 0 ? arr : dataInfoSearchPosts[key] = arr;
                    }
                    else {
                        dataInfoSearchPosts[key] = this.InfoSearchPosts[key];
                    }
                }
            }
            console.log(dataInfoSearchPosts)
            $.ajax({
                method: "POST",
                url: window.location.href,
                data: dataInfoSearchPosts
            })
                .done((msg) => {
                    listPosts.innerHTML = msg;
                    this.OnClickPagination();
                });
        }
    }

   ////// PostsSearch

    let postsSearch = new PostsSearch();
    postsSearch.createSearchPosts();
}
