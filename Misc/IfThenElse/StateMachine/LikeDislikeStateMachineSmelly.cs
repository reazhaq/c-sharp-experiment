namespace Misc.IfThenElse.StateMachine;

public class LikeDislikeStateMachineSmelly
{
    enum State
    {
        Start,
        Liked,
        Disliked
    }

    class LikeDislikeObject
    {
        public State CurrentState { get; set; } = State.Start;
        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public LikeDislikeObject(int likes, int dislikes)
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
                if (Likes > 0) { Likes--; }
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
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((5, 5), r);
        Assert.Equal(State.Start, sut.CurrentState);
    }

    [Fact]
    public void T02LikeDislike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T03LikeDislikeLike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(State.Disliked, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(State.Liked, sut.CurrentState);
    }

    [Fact]
    public void T04LikeLikeDislike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((5, 5), r);
        Assert.Equal(State.Start, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T05DislikeDislike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(State.Disliked, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 5), r);
        Assert.Equal(State.Start, sut.CurrentState);
    }

    [Fact]
    public void T06DislikeLikeDislike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(State.Disliked, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T07LikeLikeLike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(State.Start, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-3, -5), r);
        Assert.Equal(State.Liked, sut.CurrentState);
    }

    [Fact]
    public void T08LikeDislikeLike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-4, -4), r);
        Assert.Equal(State.Disliked, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-3, -4), r);
        Assert.Equal(State.Liked, sut.CurrentState);
    }

    [Fact]
    public void T09LikeLikeDislike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(State.Start, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-4, -4), r);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T10DislikeDislikeDislike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(State.Disliked, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(State.Start, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-5, -3), r);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T11DislikeLikeDislike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(State.Disliked, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-4, -4), r);
        Assert.Equal(State.Liked, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-4, -3), r);
        Assert.Equal(State.Disliked, sut.CurrentState);
    }

    [Fact]
    public void T12DislikeDislikeLike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(State.Disliked, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(State.Start, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-4, -4), r);
        Assert.Equal(State.Liked, sut.CurrentState);
    }
}
