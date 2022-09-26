namespace Misc.IfThenElse;

public class LikeDislikeStateMachineSmelly
{
    enum State
    {
        Start,
        Liked,
        Disliked
    }

    class LikeDislikeService
    {
        public State CurrentState { get; private set; } = State.Start;
        public int Likes { get; private set; }
        public int Dislikes { get; private set; }

        public LikeDislikeService(int likes, int dislikes)
        {
            Likes = likes;
            Dislikes = dislikes;
        }

        public (int likes, int dislikes) Like()
        {
            if (CurrentState == State.Start)
            {
                Likes++;
                CurrentState = State.Liked;
            }
            else if (CurrentState == State.Liked)
            {
                if(Likes > 0) { Likes--; }
                CurrentState = State.Start;
            }
            else if (CurrentState == State.Disliked)
            {
                if (Dislikes > 0) { Dislikes--; }
                Likes++;
                CurrentState = State.Liked;
            }
            return (Likes, Dislikes);
        }

        public (int likes, int dislikes) Dislike()
        {
            if (CurrentState == State.Start)
            {
                Dislikes++;
                CurrentState = State.Disliked;
            }
            else if (CurrentState == State.Liked)
            {
                if (Likes > 0) { Likes--; }
                Dislikes++;
                CurrentState = State.Disliked;
            }
            else if (CurrentState == State.Disliked)
            {
                if (Dislikes > 0) { Dislikes--; }
                CurrentState = State.Start;
            }
            return (Likes, Dislikes);
        }
    }

    [Fact]
    public void T01LikeLike()
    {
        var sut = new LikeDislikeService(5, 5);

        var result = sut.Like();
        Assert.Equal((6, 5), result);
        Assert.Equal(State.Liked, sut.CurrentState);

        result = sut.Like();
        Assert.Equal((5, 5), result);
        Assert.Equal(State.Start, sut.CurrentState);
    }

    [Fact]
    public void T02LikeDislike()
    {
        var sut = new LikeDislikeService(5, 5);

        var result = sut.Like();
        Assert.Equal((6, 5), result);
        Assert.Equal(State.Liked, sut.CurrentState);

        result = sut.Dislike();
        Assert.Equal((5, 6), result);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T03LikeDislikeLike()
    {
        var sut = new LikeDislikeService(5, 5);

        var result = sut.Like();
        Assert.Equal((6, 5), result);
        Assert.Equal(State.Liked, sut.CurrentState);

        result = sut.Dislike();
        Assert.Equal((5, 6), result);
        Assert.Equal(State.Disliked, sut.CurrentState);

        result = sut.Like();
        Assert.Equal((6, 5), result);
        Assert.Equal(State.Liked, sut.CurrentState);
    }

    [Fact]
    public void T04LikeLikeDislike()
    {
        var sut = new LikeDislikeService(5, 5);

        var result = sut.Like();
        Assert.Equal((6, 5), result);
        Assert.Equal(State.Liked, sut.CurrentState);

        result = sut.Like();
        Assert.Equal((5, 5), result);
        Assert.Equal(State.Start, sut.CurrentState);

        result = sut.Dislike();
        Assert.Equal((5, 6), result);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T05DislikeDislike()
    {
        var sut = new LikeDislikeService(5, 5);

        var result = sut.Dislike();
        Assert.Equal((5,6), result);
        Assert.Equal(State.Disliked, sut.CurrentState);

        result = sut.Dislike();
        Assert.Equal((5,5), result);
        Assert.Equal(State.Start, sut.CurrentState);
    }

    [Fact]
    public void T06DislikeLikeDislike()
    {
        var sut = new LikeDislikeService(5, 5);

        var result = sut.Dislike();
        Assert.Equal((5, 6), result);
        Assert.Equal(State.Disliked, sut.CurrentState);

        result = sut.Like();
        Assert.Equal((6, 5), result);
        Assert.Equal(State.Liked, sut.CurrentState);

        result = sut.Dislike();
        Assert.Equal((5, 6), result);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T07LikeLikeLike_starting_Negative()
    {
        var sut = new LikeDislikeService(-5, -5);

        var result = sut.Like();
        Assert.Equal((-4, -5), result);
        Assert.Equal(State.Liked, sut.CurrentState);

        result = sut.Like();
        Assert.Equal((-4, -5), result);
        Assert.Equal(State.Start, sut.CurrentState);

        result = sut.Like();
        Assert.Equal((-3, -5), result);
        Assert.Equal(State.Liked, sut.CurrentState);
    }

    [Fact]
    public void T08LikeDislikeLike_starting_Negative()
    {
        var sut = new LikeDislikeService(-5, -5);

        var result = sut.Like();
        Assert.Equal((-4, -5), result);
        Assert.Equal(State.Liked, sut.CurrentState);

        result = sut.Dislike();
        Assert.Equal((-4, -4), result);
        Assert.Equal(State.Disliked, sut.CurrentState);

        result = sut.Like();
        Assert.Equal((-3, -4), result);
        Assert.Equal(State.Liked, sut.CurrentState);
    }

    [Fact]
    public void T09LikeLikeDislike_starting_Negative()
    {
        var sut = new LikeDislikeService(-5, -5);

        var result = sut.Like();
        Assert.Equal((-4, -5), result);
        Assert.Equal(State.Liked, sut.CurrentState);

        result = sut.Like();
        Assert.Equal((-4, -5), result);
        Assert.Equal(State.Start, sut.CurrentState);

        result = sut.Dislike();
        Assert.Equal((-4, -4), result);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }
}
